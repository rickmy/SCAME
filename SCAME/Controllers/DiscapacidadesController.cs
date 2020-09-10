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
    public class DiscapacidadesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiscapacidadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Discapacidades
        public async Task<IActionResult> Index()
        {
            return View(await _context.Discapacidad.ToListAsync());
        }

        // GET: Discapacidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discapacidad = await _context.Discapacidad
                .FirstOrDefaultAsync(m => m.IdDiscapacidad == id);
            if (discapacidad == null)
            {
                return NotFound();
            }

            return View(discapacidad);
        }

        // GET: Discapacidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discapacidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDiscapacidad,NombreDiscapacidad")] Discapacidad discapacidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discapacidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discapacidad);
        }

        // GET: Discapacidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discapacidad = await _context.Discapacidad.FindAsync(id);
            if (discapacidad == null)
            {
                return NotFound();
            }
            return View(discapacidad);
        }

        // POST: Discapacidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDiscapacidad,NombreDiscapacidad")] Discapacidad discapacidad)
        {
            if (id != discapacidad.IdDiscapacidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discapacidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscapacidadExists(discapacidad.IdDiscapacidad))
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
            return View(discapacidad);
        }

        // GET: Discapacidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discapacidad = await _context.Discapacidad
                .FirstOrDefaultAsync(m => m.IdDiscapacidad == id);
            if (discapacidad == null)
            {
                return NotFound();
            }

            return View(discapacidad);
        }

        // POST: Discapacidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discapacidad = await _context.Discapacidad.FindAsync(id);
            _context.Discapacidad.Remove(discapacidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscapacidadExists(int id)
        {
            return _context.Discapacidad.Any(e => e.IdDiscapacidad == id);
        }
    }
}
