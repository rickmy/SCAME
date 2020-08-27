using System;
using System.Collections.Generic;
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
    public class MedicosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> userManager;

        public MedicosController(ApplicationDbContext context, UserManager<IdentityUser> userMgr)
        {
            _context = context;
            this.userManager = userMgr;
        }

        // GET: Medicos
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var id = user.Id;
            List<Consultorio> consultorios = await _context.Consultorio.ToListAsync();
            for (int i = 0; i < consultorios.Count; i++)
            {
                if (consultorios[i].UserId == id)
                {
                    var applicationDbContext = _context.Medicos.Include(m => m.Consultorio).Where(m => m.ConsultorioId == consultorios[i].IdConsultorio && m.Estado == true);


                    return View(await applicationDbContext.ToListAsync());
                }
            }
            return View();
        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .Include(m => m.Consultorio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles ="Consultorio")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cedula,Nombre,Apellido,Telefono,Email,CodigoSenecyt,TituloEgresado,Estado")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                List<Consultorio> listConsultorio = await _context.Consultorio.ToListAsync();
                foreach (var item in listConsultorio)
                {
                    if (item.UserId == user.Id)
                    {
                        var consultorio = item;
                        medico.ConsultorioId = consultorio.IdConsultorio;
                        medico.Estado = true;

                        _context.Add(medico);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cedula,Nombre,Apellido,Telefono,Email,CodigoSenecyt,TituloEgresado,Estado,ConsultorioId")] Medico medico)
        {
            if (id != medico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.Id))
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
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .Include(m => m.Consultorio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            medico.Estado = false;
            _context.Medicos.Update(medico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.Id == id);
        }
    }
}
