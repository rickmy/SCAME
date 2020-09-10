using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCAME.Models
{
    public class HorasAtencion
    {
        [Key]
        public int IdHorasAtencion { get; set; }
        [Display(Name = "Hora de inicio de la cita")]
        public string HoraInicio { get; set; }
        [Display(Name = "Hora de finalización de la cita")]
        public string HoraCierre { get; set; }
        [Display(Name = "Disponibilidad")]
        public bool Disponibilidad { get; set; }
        public bool Estado { get; set; }

        [DisplayName("Turno")]
        public int TurnoId { get; set; }
        public Turno Turno { get; set; }
    }
}