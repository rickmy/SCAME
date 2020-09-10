using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCAME.Models
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }
        [Display(Name ="Nombres:")]
        public string Nombre { get; set; }
        [Display(Name = "Apellidos:")]
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        [Display(Name = "Código de la Senecyt")]
        public string CodigoSenecyt { get; set; }
        [Display(Name = "Título del egresado")]
        public string TituloEgresado { get; set; }
        public bool Estado { get; set; } = true;
        public MedicoDetalle Detalle { get; set; }
        [NotMapped]
        public List<MedicoDetalle> MedicoDetalles { get; set; }
        [Display(Name = "Consultorio")]
        public int ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }

    }
}