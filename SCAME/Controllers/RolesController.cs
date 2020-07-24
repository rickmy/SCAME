using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SCAME.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SCAME.Controllers
{
    //[Authorize(Roles ="Administrador")] //se colocara que tiene acceso al controladore
    public class RolesController : Controller
    {

        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userMgr)
        {
            this.roleManager = roleManager;
            this.userManager = userMgr;
        }

        //para mostrar todos los elementos
        public ViewResult Index() => View(roleManager.Roles);

        //metodo para crear un rol devuelve la vista del servidor al cliente
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create([Required] string nombre)//se coloca el tipo y nombre de la variable que se uso en el input del html
        {
            if (ModelState.IsValid)
            {
                IdentityResult resultado = await roleManager.CreateAsync(new IdentityRole(nombre));//cuando se usa un metodo asincrono tdo tiene que ser asicrono

                if (resultado.Succeeded)
                    return RedirectToAction("Index"); //se redirijira a la vista de index
                Errores(resultado);
            }
            return View();//en caso qeu el modelsate no se valido se le regresara a l vista
        }

        private void Errores(IdentityResult resultado)
        {
            foreach (var error in resultado.Errors)//navegamos en los errores
            {
                ModelState.AddModelError("", error.Description);//ponesos los errores
            }
        }
        //metodo para eliminar el rol
        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            IdentityRole rolPorBorrar = await roleManager.FindByIdAsync(Id);
            if (rolPorBorrar != null)
            {
                IdentityResult resultado = await roleManager.DeleteAsync(rolPorBorrar);

                if (resultado.Succeeded)
                    return RedirectToAction("Index"); //se redirijira a la vista de index
                Errores(resultado);
            }
            else
            {
                ModelState.AddModelError("", "El rol no existe");
            }
            return View("Index", roleManager.Roles);
        }

        //metodo para actualizar
        public async Task<IActionResult> Update(string Id)
        {
            IdentityRole rol = await roleManager.FindByIdAsync(Id);

            List<IdentityUser> miembros = new List<IdentityUser>();
            List<IdentityUser> noMiembros = new List<IdentityUser>();

            //se va a navegar por todos los usuarios
            foreach (IdentityUser usuario in userManager.Users)
            {
                var lista = await userManager.IsInRoleAsync(usuario, rol.Name) ? miembros : noMiembros; //la incognita es como un if y si es verdadera se ejecuta lo que esta alado de la incognita y de ser flaso de ejecuta lo que esta alado de los dos puntos
                lista.Add(usuario);
            }
            var modelo = new EdicionRol
            {
                Rol = rol,
                Miembros = miembros,
                NoMiembros = noMiembros
            };
            return View(modelo);

        }
        //creamos el metod update con el meodo post con un tipo de dato modificacion rol
        [HttpPost]
        public async Task<IActionResult> Update(ModificacionRol modificacion)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                //encuentra en usuario y le pone le rol
                foreach (string userId in modificacion.AumentarIds ?? new string[] { })
                {
                    IdentityUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, modificacion.RolName);
                        if (!result.Succeeded)
                            Errores(result);
                    }
                }
                //encuentra el usuario y le remueve el rol
                foreach (string userId in modificacion.QuitarIds ?? new string[] { })
                {
                    IdentityUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, modificacion.RolName);
                        if (!result.Succeeded)
                            Errores(result);
                    }
                }
            }
            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(modificacion.RolId);
        }
    }

}
