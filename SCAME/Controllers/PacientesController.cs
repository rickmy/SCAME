using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCAME.Data;
using SCAME.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace SCAME.Controllers
{
    public class PacientesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> userManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PacientesController(ApplicationDbContext context, UserManager<IdentityUser> userMgr, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            userManager = userMgr;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Pacientes
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Paciente.Include(p => p.Canton).Include(p => p.Discapacidad).Include(p => p.Sexo).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Usuario")]
        // GET: Pacientes/Create
        public IActionResult Create()
        {
            ViewData["CantonId"] = new SelectList(_context.Canton, "Id", "NombreCanton");
            ViewData["DiscapacidadId"] = new SelectList(_context.Set<Discapacidad>(), "IdDiscapacidad", "NombreDiscapacidad");
            ViewData["SexoId"] = new SelectList(_context.Sexos, "IdSexo", "NombreSexo");
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Direccion,NumeroCarnet,Porcentaje,PdfFile,DiscapacidadId,CantonId,SexoId")] Paciente paciente)
        {
            var convertir = paciente.Cedula.ToString();
            var verificada = VerificaCedula(convertir);
            if (verificada == true) {
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(paciente.PdfFile.FileName);
                    string extension = Path.GetExtension(paciente.PdfFile.FileName);
                    paciente.PruebaCovid = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/PdfPac/", fileName);

                    var fileStream = new FileStream(path, FileMode.Create);
                    await paciente.PdfFile.CopyToAsync(fileStream);

                    paciente.UserId = userManager.GetUserId(User);
                    paciente.CasoConfirmado = false;
                    paciente.Estado = true;
                    var user = await userManager.GetUserAsync(User);
                    var userRol = await userManager.IsInRoleAsync(user, "Usuario");
                    if (userRol == true)
                    {
                        var eliminarRol = await userManager.RemoveFromRoleAsync(user, "Usuario");
                        var agregarRol = await userManager.AddToRoleAsync(user, "Paciente");
                        _context.Add(paciente);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            
            return View(paciente);
        }
        [Authorize(Roles = "Paciente")]
        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User);
            var id = user.Id;
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.Where(p=>p.UserId == id).ToListAsync();
            if (paciente[0] == null)
            {
                return NotFound();
            }
            ViewData["CantonId"] = new SelectList(_context.Canton, "Id", "NombreCanton", paciente[0].CantonId);
            ViewData["DiscapacidadId"] = new SelectList(_context.Set<Discapacidad>(), "IdDiscapacidad", "NombreDiscapacidad", paciente[0].DiscapacidadId);
            ViewData["SexoId"] = new SelectList(_context.Sexos, "IdSexo", "NombreSexo", paciente[0].SexoId);
            return View(paciente[0]);
        }
         [Authorize(Roles ="Administrador")]
        public async Task<IActionResult> CasoConfirmado(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdPaciente,Cedula,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Direccion,NumeroCarnet,Porcentaje,CasoConfirmado,DiscapacidadId,UserId,CantonId,SexoId,PruebaCovid,PdfFileNuevo")] Paciente paciente)
        {
            var user = await userManager.GetUserAsync(User);
            
            if (user.Id != paciente.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath;
                string fileName;
                string extension;
                string path;

                if (paciente.PdfFileNuevo != null && paciente.PruebaCovid != null)
                {
                    var imagenPath = Path.Combine(_hostEnvironment.WebRootPath, "PdfPac", paciente.PruebaCovid);
                    if (System.IO.File.Exists(imagenPath))
                    {
                        System.IO.File.Delete(imagenPath);

                        wwwRootPath = _hostEnvironment.WebRootPath;
                        fileName = Path.GetFileNameWithoutExtension(paciente.PdfFileNuevo.FileName);
                        extension = Path.GetExtension(paciente.PdfFileNuevo.FileName);
                        paciente.PruebaCovid = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        path = Path.Combine(wwwRootPath + "/PdfPac/", fileName);

                        var fileStream = new FileStream(path, FileMode.Create);
                        await paciente.PdfFileNuevo.CopyToAsync(fileStream);

                        _context.Update(paciente);
                        await _context.SaveChangesAsync();
                    }
                }
                wwwRootPath = _hostEnvironment.WebRootPath;
                fileName = Path.GetFileNameWithoutExtension(paciente.PdfFileNuevo.FileName);
                extension = Path.GetExtension(paciente.PdfFileNuevo.FileName);
                paciente.PruebaCovid = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                path = Path.Combine(wwwRootPath + "/PdfPac/", fileName);

                var fileStreamUno = new FileStream(path, FileMode.Create);
                await paciente.PdfFileNuevo.CopyToAsync(fileStreamUno);

                _context.Update(paciente);
                await _context.SaveChangesAsync();
                    if (!PacienteExists(paciente.IdPaciente))
                    {
                        return NotFound();
                    }
                    else
                    {
                       
                    }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CantonId"] = new SelectList(_context.Canton, "Id", "Id", paciente.CantonId);
            ViewData["DiscapacidadId"] = new SelectList(_context.Set<Discapacidad>(), "IdDiscapacidad", "IdDiscapacidad", paciente.DiscapacidadId);
            ViewData["SexoId"] = new SelectList(_context.Sexos, "IdSexo", "IdSexo", paciente.SexoId);
            return View(paciente);
        }

        public async Task<IActionResult> CasoPositivo([Required]bool casoConfirmado,int IdPaciente)
        {
            var listPacientes = await _context.Paciente.Where(p => p.Estado == true).ToListAsync(); 
            foreach (var paciente in listPacientes)
            {

                if (IdPaciente == paciente.IdPaciente)
                {
                    
                   paciente.CasoConfirmado = casoConfirmado;

                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound();
        }
        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .Include(p => p.Canton)
                .Include(p => p.Discapacidad)
                .Include(p => p.Sexo)
                .FirstOrDefaultAsync(m => m.IdPaciente == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Paciente.FindAsync(id);
            var imagenPath = Path.Combine(_hostEnvironment.WebRootPath, "PdfPac", paciente.PruebaCovid);
            if (System.IO.File.Exists(imagenPath))
                System.IO.File.Delete(imagenPath);

            paciente.Estado = false;
            paciente.PruebaCovid = null;
            var user = await userManager.GetUserAsync(User);
            var userRol = await userManager.IsInRoleAsync(user, "Paciente");
            if (userRol == true)
            {
                var eliminarRol = await userManager.RemoveFromRoleAsync(user, "Paciente");
                var agregarRol = await userManager.AddToRoleAsync(user,"Usuario"); 
               
            }
            _context.Paciente.Update(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Paciente.Any(e => e.IdPaciente == id);
        }
        public static bool VerificaCedula(string vc)
        {

            if (vc.Length == 10)
            {
                char[] validarCedula = vc.ToCharArray();
                int aux = 0, par = 0, impar = 0, verifi;
                for (int i = 0; i < 9; i += 2)
                {
                    aux = 2 * int.Parse(validarCedula[i].ToString());
                    if (aux > 9)
                        aux -= 9;
                    par += aux;
                }
                for (int i = 1; i < 9; i += 2)
                {
                    impar += int.Parse(validarCedula[i].ToString());
                }

                aux = par + impar;
                if (aux % 10 != 0)
                {
                    verifi = 10 - (aux % 10);
                }
                else
                    verifi = 0;
                if (verifi == int.Parse(validarCedula[9].ToString()))
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
