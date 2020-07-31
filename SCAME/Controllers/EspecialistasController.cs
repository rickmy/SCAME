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
    public class EspecialistasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EspecialistasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Especialistas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Especialista.ToListAsync());
        }

        // GET: Especialistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialista = await _context.Especialista
                .FirstOrDefaultAsync(m => m.Id == id);
            if (especialista == null)
            {
                return NotFound();
            }

            return View(especialista);
        }

        // GET: Especialistas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Especialistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TituloEgresado,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Cedula,CodigoSenecyt,Telefono")] Especialista especialista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(especialista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especialista);
        }

        // GET: Especialistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialista = await _context.Especialista.FindAsync(id);
            if (especialista == null)
            {
                return NotFound();
            }
            return View(especialista);
        }

        // POST: Especialistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TituloEgresado,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Cedula,CodigoSenecyt,Telefono")] Especialista especialista)
        {
            if (id != especialista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialistaExists(especialista.Id))
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
            return View(especialista);
        }

        // GET: Especialistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialista = await _context.Especialista
                .FirstOrDefaultAsync(m => m.Id == id);
            if (especialista == null)
            {
                return NotFound();
            }

            return View(especialista);
        }

        // POST: Especialistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var especialista = await _context.Especialista.FindAsync(id);
            _context.Especialista.Remove(especialista);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecialistaExists(int id)
        {
            return _context.Especialista.Any(e => e.Id == id);
        }
    }
}
