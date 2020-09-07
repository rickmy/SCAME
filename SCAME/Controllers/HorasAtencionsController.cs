using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCAME.Data;
using SCAME.Models;
using Microsoft.AspNetCore.Identity;

namespace SCAME.Controllers
{
    public class HorasAtencionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> userManager;

        public HorasAtencionsController(ApplicationDbContext context, UserManager<IdentityUser> userTur)
        {
            _context = context;
            userManager = userTur;
        }

        // GET: HorasAtencions
        public async Task<IActionResult> Index()
        {
            var user = userManager.GetUserId(User);
            var consultorio = _context.Consultorio.Where(m => m.UserId == user).ToList();
            var horas = _context.HorasAtencion.Include(h => h.Turno).Where(a => a.Turno.ConsultorioId == consultorio[0].IdConsultorio && a.Estado == true);
            return View(await horas.ToListAsync());
        }

        // GET: HorasAtencions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horasAtencion = await _context.HorasAtencion
                .FirstOrDefaultAsync(m => m.IdHorasAtencion == id);
            if (horasAtencion == null)
            {
                return NotFound();
            }

            return View(horasAtencion);
        }

        // GET: HorasAtencions/Create
        public IActionResult Create()
        {
            var user = userManager.GetUserId(User);
            var consultorio = _context.Consultorio.Where(m => m.UserId == user).ToList();

            ViewData["HorasId"] = new SelectList(_context.Turno.Where(z => z.ConsultorioId == consultorio[0].IdConsultorio), "Id", "NombreTurno");
            return View();
        }

        // POST: HorasAtencions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHorasAtencion,HoraInicio,HoraCierre,Disponibilidad,Estado,TurnoId")] HorasAtencion horasAtencion)
        {
            if (ModelState.IsValid)
            {

                horasAtencion.Disponibilidad = true;

                horasAtencion.Estado = true;
                _context.Add(horasAtencion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(horasAtencion);
        }

        // GET: HorasAtencions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horasAtencion = await _context.HorasAtencion.FindAsync(id);
            if (horasAtencion == null)
            {
                return NotFound();
            }
            ViewData["HorasId"] = new SelectList(_context.Turno, "Id", "NombreTurno");
            return View(horasAtencion);
        }

        // POST: HorasAtencions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHorasAtencion,HoraInicio,HoraCierre,TurnoId")] HorasAtencion horasAtencion)
        {
            if (id != horasAtencion.IdHorasAtencion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    horasAtencion.Disponibilidad = true;
                    horasAtencion.Estado = true;
                    _context.Update(horasAtencion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorasAtencionExists(horasAtencion.IdHorasAtencion))
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
            ViewData["HorasId"] = new SelectList(_context.Turno, "Id", "NombreTurno");
            return View(horasAtencion);
        }

        // GET: HorasAtencions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horasAtencion = await _context.HorasAtencion
                .FirstOrDefaultAsync(m => m.IdHorasAtencion == id);
            if (horasAtencion == null)
            {
                return NotFound();
            }

            return View(horasAtencion);
        }

        // POST: HorasAtencions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horasAtencion = await _context.HorasAtencion.FindAsync(id);
            horasAtencion.Estado = false;

            _context.HorasAtencion.Update(horasAtencion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorasAtencionExists(int id)
        {
            return _context.HorasAtencion.Any(e => e.IdHorasAtencion == id);
        }
    }
}
