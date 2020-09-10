using System;
using System.ComponentModel.DataAnnotations;

namespace SCAME.Models
{
    public class Horario
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Dia")]
        public string Dia { get; set; }
        [Display(Name = "Hora de apertura")]
        public string HoraApertura { get; set; }
        [Display(Name = "Hora de cierre")]
        public string HoraCierre { get; set; }
        public bool Estado { get; set; } = true;
        [Display(Name = "Consultorio")]
        public int ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }

    }
}