using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCAME.Models
{
    public class HorasAtencion
    {
        [Key]
        public int IdHorasAtencion { get; set; }
        public string HoraInicio { get; set; }
        public string HoraCierre { get; set; }
        public bool Disponibilidad { get; set; }
        public bool Estado { get; set; }

        [DisplayName("Turno")]
        public int TurnoId { get; set; }
        public Turno Turno { get; set; }
    }
}