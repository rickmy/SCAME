using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCAME.Data;
using SCAME.Models;

namespace SCAME.Controllers
{
    public class MedicoEspecialidadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicoEspecialidadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MedicoEspecialidads
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConsultorioDetalle.Include(m => m.Especialidad).Include(m => m.Medico);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MedicoEspecialidads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicoEspecialidad = await _context.ConsultorioDetalle
                .Include(m => m.Especialidad)
                .Include(m => m.Medico)
                .FirstOrDefaultAsync(m => m.IdMedicoEspecialidad == id);
            if (medicoEspecialidad == null)
            {
                return NotFound();
            }

            return View(medicoEspecialidad);
        }

        // GET: MedicoEspecialidads/Create
        public IActionResult Create()
        {
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Id");
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Id");
            return View();
        }

        // POST: MedicoEspecialidads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMedicoEspecialidad,MedicoId,EspecialidadId,PrecioEspecialidad,DescripcionEspecialidad,Estado")] MedicoEspecialidad medicoEspecialidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicoEspecialidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Id", medicoEspecialidad.EspecialidadId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Id", medicoEspecialidad.MedicoId);
            return View(medicoEspecialidad);
        }

        // GET: MedicoEspecialidads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicoEspecialidad = await _context.ConsultorioDetalle.FindAsync(id);
            if (medicoEspecialidad == null)
            {
                return NotFound();
            }
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Id", medicoEspecialidad.EspecialidadId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Id", medicoEspecialidad.MedicoId);
            return View(medicoEspecialidad);
        }

        // POST: MedicoEspecialidads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMedicoEspecialidad,MedicoId,EspecialidadId,PrecioEspecialidad,DescripcionEspecialidad,Estado")] MedicoEspecialidad medicoEspecialidad)
        {
            if (id != medicoEspecialidad.IdMedicoEspecialidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicoEspecialidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoEspecialidadExists(medicoEspecialidad.IdMedicoEspecialidad))
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
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Id", medicoEspecialidad.EspecialidadId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Id", medicoEspecialidad.MedicoId);
            return View(medicoEspecialidad);
        }

        // GET: MedicoEspecialidads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicoEspecialidad = await _context.ConsultorioDetalle
                .Include(m => m.Especialidad)
                .Include(m => m.Medico)
                .FirstOrDefaultAsync(m => m.IdMedicoEspecialidad == id);
            if (medicoEspecialidad == null)
            {
                return NotFound();
            }

            return View(medicoEspecialidad);
        }

        // POST: MedicoEspecialidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicoEspecialidad = await _context.ConsultorioDetalle.FindAsync(id);
            _context.ConsultorioDetalle.Remove(medicoEspecialidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoEspecialidadExists(int id)
        {
            return _context.ConsultorioDetalle.Any(e => e.IdMedicoEspecialidad == id);
        }
    }
}
