using System;
using System.ComponentModel.DataAnnotations;

namespace SCAME.Models
{
    public class Turno
    {
        [Key]
        public int Id { get; set; }
        public string NombreTurno { get; set; }
        public string DiasTurno { get; set; }
        public string HoraInicio { get; set; }
        public string HoraSalida { get; set; }
        public bool Estado { get; set; }
        public int ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }
    }
}