using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCAME.Data;
using SCAME.Models;

namespace SCAME.Controllers
{
    public class TurnosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public TurnosController(ApplicationDbContext context, UserManager<IdentityUser> userMng)
        {
            _context = context;
            userManager = userMng;
        }

        // GET: Turnos
        public async Task<IActionResult> Inicio()
        {
            var user = await userManager.GetUserAsync(User);
            var id = user.Id;
            List<Consultorio> consultorios = await _context.Consultorio.ToListAsync();
            for (int i = 0; i < consultorios.Count; i++)
            {
                if (consultorios[i].UserId == id)
                {
                    var applicationDbContext = _context.Turno.Include(t => t.Consultorio).Where(t => t.ConsultorioId == consultorios[i].IdConsultorio && t.Estado == true);


                    return View(await applicationDbContext.ToListAsync());
                }
            }
            return View();
        }

        // GET: Turnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno
                .Include(t => t.Consultorio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turnos/Create
        public IActionResult Create()
        {
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "IdConsultorio", "IdConsultorio");
            return View();
        }

        // POST: Turnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreTurno,DiasTurno,HoraInicio,HoraSalida,Estado")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);

                List<Consultorio> lstConsultorio = await _context.Consultorio.ToListAsync();
                for (int i = 0; i < lstConsultorio.Count; i++)
                {
                    if(lstConsultorio[i].UserId == user.Id)
                    {
                        int idConsultorio = lstConsultorio[i].IdConsultorio;
                        turno.ConsultorioId = idConsultorio;
                        turno.Estado = true;
                    }
                }
                string idUser = user.Id;
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction("Inicio","Turnos",new {@id=idUser });
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "IdConsultorio", "IdConsultorio", turno.ConsultorioId);
            return View(turno);
        }

        // GET: Turnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "IdConsultorio", "IdConsultorio", turno.ConsultorioId);
            return View(turno);
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreTurno,DiasTurno,HoraInicio,HoraSalida,Estado,ConsultorioId")] Turno turno)
        {
            if (id != turno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Inicio));
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorio, "IdConsultorio", "IdConsultorio", turno.ConsultorioId);
            return View(turno);
        }

        // GET: Turnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno
                .Include(t => t.Consultorio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turno = await _context.Turno.FindAsync(id);
            _context.Turno.Remove(turno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Inicio));
        }

        private bool TurnoExists(int id)
        {
            return _context.Turno.Any(e => e.Id == id);
        }
    }
}
