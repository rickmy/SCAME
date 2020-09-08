using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCAME.Models
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }
        public string Cedula { get; set; }
        [Display(Name ="Nombres:")]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string CodigoSenecyt { get; set; }
        public string TituloEgresado { get; set; }
        public bool Estado { get; set; } = true;
        public MedicoDetalle Detalle { get; set; }
        public int ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }

    }
}