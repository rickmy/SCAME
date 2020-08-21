using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCAME.Data;
using SCAME.Models;

namespace SCAME.Controllers
{
    public class ConsultoriosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> userManager;

        public ConsultoriosController(ApplicationDbContext context, UserManager<IdentityUser> userMgr)
        {
            _context = context;
            this.userManager = userMgr;
        }



        [Authorize(Roles = "Administrador")]
        // GET: Consultorios
        public async Task<IActionResult> Index()
        {

            return View(await _context.Consultorio.Include(c=>c.Canton).Include(c=>c.User).ToListAsync());
        }

        // GET: Consultorios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultorio = await _context.Consultorio
                .FirstOrDefaultAsync(m => m.IdConsultorio == id);

            var canton = await _context.Canton.FindAsync(consultorio.CantonId);
            var usuario = await userManager.FindByIdAsync(consultorio.UserId);

            consultorio.Canton = canton;
            consultorio.User = usuario;

            if (consultorio == null)
            {
                return NotFound();
            }

            return View(consultorio);
        }
        [Authorize(Roles = "Usuario, Administrador")]
        // GET: Consultorios/Create
        public IActionResult Create()
        {
            ViewData["CantonId"] = new SelectList(_context.Set<Canton>(), "Id", "NombreCanton");

            return View();
        }

        // POST: Consultorios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Usuario, Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Required] string Ruc, string NombreConsultorio, string CedulaRepresentanteLegal, string NombreRepresentateLegal, string ApellidoRepresentanteLegal, string Direccion, string NumPatenteMunicipal, string PermisoFuncionamientoMsp, int CantonId)
        {
            
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);

                var consultorio = new Consultorio();
                {
                    consultorio.Ruc = Ruc;
                    consultorio.NombreConsultorio = NombreConsultorio;
                    consultorio.CedulaRepresentanteLegal = CedulaRepresentanteLegal;
                    consultorio.NombreRepresentateLegal = NombreRepresentateLegal;
                    consultorio.ApellidoRepresentanteLegal = ApellidoRepresentanteLegal;
                    consultorio.Direccion = Direccion;
                    consultorio.PermisoFuncionamientoMsp = PermisoFuncionamientoMsp;
                    consultorio.NumPatenteMunicipal = NumPatenteMunicipal;
                    consultorio.CantonId = CantonId;
                    consultorio.UserId = await userManager.GetUserIdAsync(user);
                }
                if(consultorio != null)
                {
                    var userRol = await userManager.IsInRoleAsync(user, "Usuario");
                    if (userRol == true)
                    {
                        var eliminarRol = await userManager.RemoveFromRoleAsync(user, "Usuario");
                        var result1 = await userManager.AddToRoleAsync(user, "Consultorio");
                        _context.Add(consultorio);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }

        

        [Authorize(Roles = "Consultorio")]
        // GET: Consultorios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            List<Consultorio> listConsultorio = await _context.Consultorio.ToListAsync();
            foreach (var item in listConsultorio)
            {
                if (item.UserId == id)
                {
                    var consultorio = item;

                    if (consultorio == null)
                    {
                        return NotFound();
                    }
                    ViewData["CantonId"] = new SelectList(_context.Set<Canton>(), "Id", "NombreCanton",consultorio.CantonId);

                    return View(consultorio);
                }
            }
            return NotFound();
        }

        // POST: Consultorios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Ruc,NombreConsultorio,CedulaRepresentanteLegal,NombreRepresentateLegal,ApellidoRepresentanteLegal,Direccion,NumPatenteMunicipal,PermisoFuncionamientoMsp,CantonId,UserId,IdConsultorio")] Consultorio consultorio)
        {

            if (id != consultorio.UserId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultorioExists(consultorio.IdConsultorio))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CantonId"] = new SelectList(_context.Set<Canton>(), "Id", "NombreCanton", consultorio.CantonId);

            return View(consultorio);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Consultorios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultorio = await _context.Consultorio
                .FirstOrDefaultAsync(m => m.IdConsultorio == id);
            if (consultorio == null)
            {
                return NotFound();
            }

            return View(consultorio);
        }

        // POST: Consultorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultorio = await _context.Consultorio.FindAsync(id);
            _context.Consultorio.Remove(consultorio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultorioExists(int id)
        {
            return _context.Consultorio.Any(e => e.IdConsultorio == id);
        }
    }
}
