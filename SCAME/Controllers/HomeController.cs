using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCAME.Models;
using SCAME.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SCAME.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> userManager;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, UserManager<IdentityUser> userMgr)
        {
            _logger = logger;
            _context = context;
            userManager = userMgr;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Search(string especialidad)
        {
            if (especialidad != null)
            {
                var listConsultorio = _context.Consultorio.Where(c => c.Estado == true).ToList();
                var listEspecialidades = _context.Especialidad.Where(e=>e.Estado == true && e.NombreEspecialidad.Contains(especialidad)).ToList();
                var listDetalles = _context.MedicoDetalle.ToList();
                List<Consultorio> listConsultorioBusqueda = new List<Consultorio>();

                for (int i = 0; i < listConsultorio.Count(); i++)
                {
                    for (int j = 0; j < listDetalles.Count(); j++)
                    {
                        for (int k = 0; k < listEspecialidades.Count(); k++)
                        {
                            if (listDetalles[j].ConsultorioId == listConsultorio[i].IdConsultorio && listDetalles[j].EspecialidadId == listEspecialidades[k].Id)
                            {
                                listConsultorioBusqueda.Add(listConsultorio[i]);
                                return View("Busqueda", listConsultorioBusqueda);
                            }
                        }
                    }
                }

                //var espe = _context.Consultorio.Where(h => h.Medicos.Detalle.Especialidad.NombreEspecialidad.Contains(especialidad));
                ////var lista = espe.ToList();

                //return View("Busqueda", espe.ToList());
            }
            return View("Index");
        }
        public IActionResult TraerCitas()
        {
            var user = userManager.GetUserId(User);

            var citas = _context.Cita.Include(m => m.Consultorio).Include(m => m.Paciente).Where(c => c.Consultorio.UserId == user).ToList();

            var listaCitas = citas.Select(x => new Models.HomeCalendario
            {
                Id = x.IdCita,
                Title = $"{x.Paciente.PrimerApellido} {x.Paciente.PrimerNombre}",
                Url = $"/Citas/Details/{x.IdCita}",
                Start = x.FechaCita.ToString("yyyy-MM-dd"),
                AllDay = false,
                Color = "FFFFFF",
                TextColor = "000000"
            }).ToList();
            return (Json(new
            {
                Data = listaCitas,
                IsSuccessful = true
            }));
        }
    }
}
