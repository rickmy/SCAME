using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCAME.Data;
using SCAME.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Authentication;

namespace SCAME.Controllers
{
    public class HorariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> userManager;

        public HorariosController(ApplicationDbContext context, UserManager<IdentityUser> userMgr)
        {
            _context = context;
            this.userManager = userMgr;
        }

        // GET: Horarios
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            List<Consultorio> consultorios = await _context.Consultorio.ToListAsync();
            foreach (var item in consultorios)
            {
                if (item.UserId == user.Id)
                {
                    var applicationDbContext = _context.Horario.Include(h => h.Consultorio).
                        Where(h => h.ConsultorioId == item.IdConsultorio && h.Estado == true);
                    return View(await applicationDbContext.ToListAsync());
                }

            }
            return View();

            //var applicationDbContext = _context.Horario.Include(h => h.Consultorio);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Horarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario = await _context.Horario
                .Include(h => h.Consultorio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horario == null)
            {
                return NotFound();
            }

            return View(horario);
        }

        // GET: Horarios/Create
        public async Task<IActionResult> Create()
        {
            List<Horario> horarios = _context.Horario.ToList();
            List<Consultorio> consultorios = _context.Consultorio.ToList();
            var user = await userManager.GetUserAsync(User);
            for (int i = 0; i < consultorios.Count; i++)
            {
                if (user.Id == consultorios[i].UserId)
                {
                    int consulId = consultorios[i].IdConsultorio;
                    for (int j = 0; j < horarios.Count; j++)
                    {
                        if (consulId == horarios[j].ConsultorioId)
                        {
                            ViewData["primerizo"] = false;
                            return View();
                        }
                    }
                }
            }
            ViewData["primerizo"] = true;
            return View();
        }

        // POST: Horarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Dia,HoraApertura,HoraCierre,Estado,ConsultorioId")] Horario horario)
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
                        horario.ConsultorioId = consultorio.IdConsultorio;
                        horario.Estado = true;

                        _context.Add(horario);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "IdConsultorio", "IdConsultorio", horario.ConsultorioId);
            return View(horario);
        }

        public async Task<IActionResult> Create1([Bind("Id,Dia,HoraApertura,HoraCierre,Estado,ConsultorioId")] Horario horario)
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
                        horario.ConsultorioId = consultorio.IdConsultorio;
                        horario.Estado = true;

                        _context.Add(horario);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Account", "Identity",new {@id = "Manage" });
                    }
                }
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "IdConsultorio", "IdConsultorio", horario.ConsultorioId);
            return View(horario);
        }
        // GET: Horarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario = await _context.Horario.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "IdConsultorio", "IdConsultorio", horario.ConsultorioId);
            return View(horario);
        }

        // POST: Horarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dia,HoraApertura,HoraCierre,Estado")] Horario horario)
        {
            if (id != horario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                try
                {
                    List<Consultorio> listConsultorio = await _context.Consultorio.ToListAsync();
                    foreach (var item in listConsultorio)
                    {
                        if (item.UserId == user.Id)
                        {
                            var consultorio = item;
                            horario.ConsultorioId = consultorio.IdConsultorio;
                            _context.Update(horario);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //_context.Update(horario);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioExists(horario.Id))
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
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "IdConsultorio", "IdConsultorio", horario.ConsultorioId);
            return View(horario);
        }

        // GET: Horarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario = await _context.Horario
                .Include(h => h.Consultorio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horario == null)
            {
                return NotFound();
            }

            return View(horario);
        }

        // POST: Horarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horario = await _context.Horario.FindAsync(id);
            horario.Estado = false;
            _context.Horario.Update(horario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioExists(int id)
        {
            return _context.Horario.Any(e => e.Id == id);
        }
    }
}
