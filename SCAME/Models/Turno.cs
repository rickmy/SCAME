using System;
using System.ComponentModel.DataAnnotations;

namespace SCAME.Models
{
    public class Turno
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nombre del turno")]
        public string NombreTurno { get; set; }
        [Display(Name = "Días del turno")]
        public string DiasTurno { get; set; }
        [Display(Name = "Hora de inicio del turno")]
        public string HoraInicio { get; set; }
        [Display(Name = "Hora de fin del turno")]
        public string HoraSalida { get; set; }
        public bool Estado { get; set; }
        [Display(Name = "Consultorio")]
        public int ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }
    }
}