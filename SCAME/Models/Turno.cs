using System;
using System.ComponentModel.DataAnnotations;

namespace SCAME.Models
{
    public class Turno
    {
        [Key]
        public int Id { get; set; }
        public string NombreTurno { get; set; }
        public DateTime DiasTurno { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraSalida { get; set; }
        public bool Estado { get; set; }

    }
}