using System;

namespace SCAME.Models
{
    public class Horario
    {
        public int Id { get; set; }
        public string Dia { get; set; }
        public DateTime Fecha { get; set; }
        public string Disponibilidad { get; set; }
    }
}