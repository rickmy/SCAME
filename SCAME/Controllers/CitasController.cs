using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class CitasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> userManager;


        public CitasController(ApplicationDbContext context, UserManager<IdentityUser> userMgr)
        {
            _context = context;
            this.userManager = userMgr;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var listPaciente = await _context.Paciente.Where(p => p.UserId == user.Id).ToListAsync();

            var applicationDbContext = _context.Cita.Include(c => c.Especialidad).Include(c => c.HorasAtencion).Include(c=>c.Medico).Include(c=>c.Consultorio).Include(c => c.Paciente).Where(c=>c.PacienteId == listPaciente[0].IdPaciente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita
                .Include(c => c.Especialidad)
                .Include(c => c.HorasAtencion)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create(int idC, int idM,int idE)
        {

            var detalle = _context.MedicoDetalle.Where(d => d.ConsultorioId == idC).ToList();
            ViewData["ConsultorioId"] = idC;
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad.Where(e=>e.MedicoDetalle.IdMedicoDetalle == detalle[0].IdMedicoDetalle), "Id", "NombreEspecialidad");
            ViewData["MedicoId"] = new SelectList(_context.Medicos.Where(m => m.ConsultorioId == idC), "Id", "Nombre");

            ViewData["HorasAtencionId"] = new SelectList(_context.HorasAtencion.Where(ha => ha.Disponibilidad == true && ha.Turno.ConsultorioId == idC), "IdHorasAtencion", "HoraInicio");
           return View();
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FechaCita,MedicoId,MotivoCita,EspecialidadId,ConsultorioId,HorasAtencionId")] Cita cita)
        {
            var user = await userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {

            
            var listPaciente = await _context.Paciente.Where(p=>p.UserId == user.Id).ToListAsync();
            var horadisponible = await _context.HorasAtencion.Where(h => h.IdHorasAtencion == cita.HorasAtencionId).ToListAsync();

                foreach (var item in listPaciente)
            {
                    
                    cita.PacienteId = item.IdPaciente;
                    cita.EstadoCita = "Activa";
                    cita.Estado = true;
                    _context.Add(cita);
                    horadisponible[0].Disponibilidad = false;
                    _context.Update(horadisponible[0]);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                
                ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Id", cita.EspecialidadId);
                ViewData["HorasAtencionId"] = new SelectList(_context.HorasAtencion, "IdHorasAtencion", "IdHorasAtencion", cita.HorasAtencionId);
                ViewData["PacienteId"] = new SelectList(_context.Paciente, "IdPaciente", "IdPaciente", cita.PacienteId);
                return View(cita);
            }
            }
            return NotFound();
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Id", cita.EspecialidadId);
            ViewData["HorasAtencionId"] = new SelectList(_context.HorasAtencion, "IdHorasAtencion", "IdHorasAtencion", cita.HorasAtencionId);
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "IdPaciente", "IdPaciente", cita.PacienteId);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,PacienteId,FechaCita,MedicoId,MotivoCita,MotivoAnulacion,EspecialidadId,ConsultorioId,HorasAtencionId,EstadoCita,Estado")] Cita cita)
        {
            if (id != cita.IdCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.IdCita))
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
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Id", cita.EspecialidadId);
            ViewData["HorasAtencionId"] = new SelectList(_context.HorasAtencion, "IdHorasAtencion", "IdHorasAtencion", cita.HorasAtencionId);
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "IdPaciente", "IdPaciente", cita.PacienteId);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita
                .Include(c => c.Especialidad)
                .Include(c => c.HorasAtencion)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Cita.FindAsync(id);
            _context.Cita.Remove(cita);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Cita.Any(e => e.IdCita == id);
        }
    }
}
