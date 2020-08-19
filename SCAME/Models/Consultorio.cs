using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Models
{
    public class Consultorio
    {
        [Key]
        public int Id { get; set; }
        public string Ruc { get; set; }
        public string NombreConsultorio { get; set; }
        public string CedulaRepresentanteLegal { get; set; }
        public string NombreRepresentateLegal { get; set; }
        public string ApellidoRepresentanteLegal { get; set; }
        public string Direccion { get; set; }
        public string NumPatenteMunicipal { get; set; }
        public string PermisoFuncionamientoMsp { get; set; }
        public List<Horario> Horarios { get; set; } = new List<Horario>();
        public int CantonId { get; set; }
        public string Canton { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public IdentityUser User { get; set; }
        public bool Estado { get; set; } = true;
    }
}
