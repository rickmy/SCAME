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
    public class MedicoDetallesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> userManager;


        public MedicoDetallesController(ApplicationDbContext context, UserManager<IdentityUser> userMgr)
        {
            _context = context;
            this.userManager = userMgr;
        }

        // GET: MedicoDetalles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MedicoDetalle.Include(m => m.Especialidad).Include(m => m.Medico).Include(m => m.Turno);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MedicoDetalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicoDetalle = await _context.MedicoDetalle
                .Include(m => m.Especialidad)
                .Include(m => m.Medico)
                .Include(m => m.Turno)
                .FirstOrDefaultAsync(m => m.IdMedicoDetalle == id);
            if (medicoDetalle == null)
            {
                return NotFound();
            }

            return View(medicoDetalle);
        }

        // GET: MedicoDetalles/Create
        public IActionResult Create()
        {
            var user = userManager.GetUserId(User);

            var consultorio = _context.Consultorio.Where(c => c.UserId == user).ToList();

            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "NombreEspecialidad");
            ViewData["MedicoId"] = new SelectList(_context.Medicos.Where(m=>m.ConsultorioId == consultorio[0].IdConsultorio), "Id", "Nombre");
            ViewData["TurnoId"] = new SelectList(_context.Turno.Where(t=>t.ConsultorioId == consultorio[0].IdConsultorio), "Id", "NombreTurno");
            return View();

        }

        // POST: MedicoDetalles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMedicoDetalle,MedicoId,EspecialidadId,PrecioEspecialidad,DescripcionEspecialidad,TurnoId,Estado")] MedicoDetalle medicoDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicoDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Id", medicoDetalle.EspecialidadId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Id", medicoDetalle.MedicoId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Id", medicoDetalle.TurnoId);
            return View(medicoDetalle);
        }

        // GET: MedicoDetalles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicoDetalle = await _context.MedicoDetalle.FindAsync(id);
            if (medicoDetalle == null)
            {
                return NotFound();
            }
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Id", medicoDetalle.EspecialidadId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Id", medicoDetalle.MedicoId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Id", medicoDetalle.TurnoId);
            return View(medicoDetalle);
        }

        // POST: MedicoDetalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMedicoDetalle,MedicoId,EspecialidadId,PrecioEspecialidad,DescripcionEspecialidad,TurnoId,Estado")] MedicoDetalle medicoDetalle)
        {
            if (id != medicoDetalle.IdMedicoDetalle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicoDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoDetalleExists(medicoDetalle.IdMedicoDetalle))
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
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Id", medicoDetalle.EspecialidadId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Id", medicoDetalle.MedicoId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Id", medicoDetalle.TurnoId);
            return View(medicoDetalle);
        }

        // GET: MedicoDetalles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicoDetalle = await _context.MedicoDetalle
                .Include(m => m.Especialidad)
                .Include(m => m.Medico)
                .Include(m => m.Turno)
                .FirstOrDefaultAsync(m => m.IdMedicoDetalle == id);
            if (medicoDetalle == null)
            {
                return NotFound();
            }

            return View(medicoDetalle);
        }

        // POST: MedicoDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicoDetalle = await _context.MedicoDetalle.FindAsync(id);
            _context.MedicoDetalle.Remove(medicoDetalle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoDetalleExists(int id)
        {
            return _context.MedicoDetalle.Any(e => e.IdMedicoDetalle == id);
        }
    }
}
