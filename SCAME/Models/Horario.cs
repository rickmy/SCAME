using System;

namespace SCAME.Models
{
    public class Horario
    {
        public int Id { get; set; }
        public string Dia { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime HoraApertura { get; set; }
        public DateTime HoraCierre { get; set; }
        public bool Estado { get; set; } = true;



    }
}