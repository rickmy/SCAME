using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IWebHostEnvironment _hostEnvironment;

        public ConsultoriosController(ApplicationDbContext context, UserManager<IdentityUser> userMgr, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.userManager = userMgr;
            this._hostEnvironment = hostEnvironment;
        }



        [Authorize(Roles = "Administrador")]
        // GET: Consultorios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext =_context.Consultorio.Include(c => c.Canton).Include(c => c.User).Where(m => m.Estado == true);


            return View(await applicationDbContext.ToListAsync());
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
        public async Task<IActionResult> Create([Bind ("Ruc, NombreConsultorio, CedulaRepresentanteLegal, NombreRepresentateLegal, ApellidoRepresentanteLegal, Direccion, NumPatenteMunicipal,  PermisoFuncionamientoMsp,  CantonId, ImageFile")] Consultorio consultorio)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);

                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(consultorio.ImageFile.FileName);
                string extension = Path.GetExtension(consultorio.ImageFile.FileName);
                consultorio.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                var fileStream = new FileStream(path, FileMode.Create);
                await consultorio.ImageFile.CopyToAsync(fileStream);


                consultorio.UserId = await userManager.GetUserIdAsync(user);

                var userRol = await userManager.IsInRoleAsync(user, "Usuario");
                if (userRol == true)
                {
                    var eliminarRol = await userManager.RemoveFromRoleAsync(user, "Usuario");
                    var result1 = await userManager.AddToRoleAsync(user, "Consultorio");
                    _context.Add(consultorio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create", "Horarios");
                }

            }
            return View();
        }



        [Authorize(Roles = "Consultorio")]
        // GET: Consultorios/Edit/5
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User);
            var id = user.Id;
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
        public async Task<IActionResult> Edit([Bind("Ruc,NombreConsultorio,CedulaRepresentanteLegal,NombreRepresentateLegal,ApellidoRepresentanteLegal,Direccion,NumPatenteMunicipal,PermisoFuncionamientoMsp,CantonId,UserId,IdConsultorio,ImageFileNuevo, ImageName")] Consultorio consultorio)
        {
            var user = await userManager.GetUserAsync(User);
            var id = user.Id;
            if (id != consultorio.UserId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                string wwwRootPath;
                string fileName;
                string extension;
                string path;

                if (consultorio.ImageFileNuevo != null && consultorio.ImageName != null)
                {
                    var imagenPath = Path.Combine(_hostEnvironment.WebRootPath, "Image", consultorio.ImageName);
                    if (System.IO.File.Exists(imagenPath))
                    {
                        System.IO.File.Delete(imagenPath);

                        wwwRootPath = _hostEnvironment.WebRootPath;
                        fileName = Path.GetFileNameWithoutExtension(consultorio.ImageFileNuevo.FileName);
                        extension = Path.GetExtension(consultorio.ImageFileNuevo.FileName);
                        consultorio.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        var fileStream = new FileStream(path, FileMode.Create);
                        await consultorio.ImageFileNuevo.CopyToAsync(fileStream);

                        _context.Update(consultorio);
                        await _context.SaveChangesAsync();
                    }
                }
                wwwRootPath = _hostEnvironment.WebRootPath;
                fileName = Path.GetFileNameWithoutExtension(consultorio.ImageFileNuevo.FileName);
                extension = Path.GetExtension(consultorio.ImageFileNuevo.FileName);
                consultorio.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                path = Path.Combine(wwwRootPath + "/Image/", fileName);

                var fileStreamUno = new FileStream(path, FileMode.Create);
                await consultorio.ImageFileNuevo.CopyToAsync(fileStreamUno);

                _context.Update(consultorio);
                await _context.SaveChangesAsync();
                if (!ConsultorioExists(consultorio.IdConsultorio))
                {
                    return NotFound();
                }
                else
                {

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

            var imagenPath = Path.Combine(_hostEnvironment.WebRootPath, "Image", consultorio.ImageName);
            if (System.IO.File.Exists(imagenPath))
                System.IO.File.Delete(imagenPath);

            consultorio.Estado = false;
            consultorio.ImageName = null;

            var user = await userManager.GetUserAsync(User);
            var userRol = await userManager.IsInRoleAsync(user, "Consultorio");
            if (userRol == true)
            {
                var eliminarRol = await userManager.RemoveFromRoleAsync(user, "Consultorio");
                var agregarRol = await userManager.AddToRoleAsync(user, "Usuario");

            }
            _context.Consultorio.Update(consultorio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultorioExists(int id)
        {
            return _context.Consultorio.Any(e => e.IdConsultorio == id);
        }
        public async Task<IActionResult> VistaComercial(int? id)
        {
            var applicationDbContext = _context.Consultorio.Include(c => c.Canton).Include(c => c.User).Include(c => c.Medicos).Include(c => c.Medicos.Detalle).Include(c => c.Medicos.Detalle.Especialidad).Where(m => m.Estado == true && m.IdConsultorio == id);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
