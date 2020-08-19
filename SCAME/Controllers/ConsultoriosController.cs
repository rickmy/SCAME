﻿using System;
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

            return View(await _context.Consultorio.ToListAsync());
        }

        // GET: Consultorios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultorio = await _context.Consultorio
                .FirstOrDefaultAsync(m => m.Id == id);
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
            List<Canton> ciudades = _context.Canton.ToList();
            List<SelectListItem> items = ciudades.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombreCanton.ToString(),
                    Value = d.Id.ToString(),
                    
                    Selected = false
                };

            });

            ViewBag.items = items;

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
            var user = await userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var nombreCanton = _context.Canton.Where(ca => ca.Id == CantonId).FirstOrDefault<Canton>();
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
                    consultorio.Canton = nombreCanton.NombreCanton;
                    consultorio.UserId = await userManager.GetUserIdAsync(user);
                    consultorio.Email = await userManager.GetEmailAsync(user);
                    consultorio.Telefono = await userManager.GetPhoneNumberAsync(user);
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultorio = await _context.Consultorio.FindAsync(id);
            if (consultorio == null)
            {
                return NotFound();
            }
            return View(consultorio);
        }

        // POST: Consultorios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ruc,NombreConsultorio,CedulaRepresentanteLegal,NombreRepresentateLegal,ApellidoRepresentanteLegal,Direccion,NumPatenteMunicipal,PermisoFuncionamientoMsp")] Consultorio consultorio)
        {
            if (id != consultorio.Id)
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
                    if (!ConsultorioExists(consultorio.Id))
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
                .FirstOrDefaultAsync(m => m.Id == id);
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
            return _context.Consultorio.Any(e => e.Id == id);
        }
    }
}
