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
    public class Paciente
    {
        [Key]
        [DisplayName("Id")]
        public int IdPaciente { get; set; }
        [DisplayName("Cedula")]
        public string Cedula { get; set; }
        [DisplayName("Nombres")]
        public string PrimerNombre { get; set; }        
        public string SegundoNombre { get; set; }
        [DisplayName("Apellidos")]
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        [DisplayName("Dirección")]
        public string Direccion { get; set; }
        [DisplayName("Prueba de Covid-19")]
        public string PruebaCovid { get; set; }
        [NotMapped]
        [DisplayName("Subir Prueba de Covid-19:")]
        public IFormFile PdfFile { get; set; }
        [NotMapped]
        [DisplayName("Actualizar Prueba de Covid-19:")]
        public IFormFile PdfFileNuevo { get; set; }
        public bool Estado { get; set; } = true;
        [DisplayName("¿Caso Confirmado?")]
        public bool CasoConfirmado { get; set; }
        [DisplayName("Numero de carnet de discapacidad")]
        public string NumeroCarnet { get; set; }
        [DisplayName("Porcentaje de discapacidad")]
        public string Porcentaje { get; set; }
        public int DiscapacidadId { get; set; }
        [DisplayName("Discapacidad")]
        public Discapacidad Discapacidad { get; set; }
        [DisplayName("Canton")]
        public int CantonId { get; set; }
        public Canton Canton { get; set; }
        [DisplayName("Sexo")]
        public int SexoId { get; set; }
        public Sexo Sexo { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
