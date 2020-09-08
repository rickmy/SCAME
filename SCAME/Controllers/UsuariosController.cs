using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UsuariosController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userMgr)
        {
            this.roleManager = roleManager;
            this.userManager = userMgr;
        }

        // GET: Usuarios con el rol usuarios unicamente

        public async Task<IActionResult> Index()
        {
            List<IdentityUser> miembros = new List<IdentityUser>();
            List<IdentityUser> noMiembros = new List<IdentityUser>();
            foreach (IdentityUser usuario in userManager.Users)
            {
                var lista = await userManager.IsInRoleAsync(usuario, "Usuario") ? miembros : noMiembros; //la incognita es como un if y si es verdadera se ejecuta lo que esta alado de la incognita y de ser flaso de ejecuta lo que esta alado de los dos puntos
                lista.Add(usuario);
            }
            return View(miembros);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
