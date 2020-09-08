using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Models
{
    public class Consultorio
    {
        [Key]
        public int IdConsultorio { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Imagen")]
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Subir Archivo:")]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        [DisplayName("Actualizar Imagen:")]
        public IFormFile ImageFileNuevo { get; set; }
        public string Ruc { get; set; }
        [DisplayName("Nombre Consultorio")]
        public string NombreConsultorio { get; set; }
        [DisplayName("Cedula del Representante")]
        public string CedulaRepresentanteLegal { get; set; }
        [DisplayName("Nombre Representante")]
        public string NombreRepresentateLegal { get; set; }
        public string ApellidoRepresentanteLegal { get; set; }
        public string Direccion { get; set; }
        [DisplayName("Patente Municipal")]
        public string NumPatenteMunicipal { get; set; }
        [DisplayName("Permiso Funcionamiento MSP")]
        public string PermisoFuncionamientoMsp { get; set; }
        public List<Horario> Horarios { get; set; } = new List<Horario>();
        public Medico Medicos { get; set; }
        public int CantonId { get; set; }
        public Canton Canton { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public bool Estado { get; set; } = true;
    }
}
