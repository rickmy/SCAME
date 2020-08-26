using System;

namespace SCAME.Models
{
    public class Horario
    {
        public int Id { get; set; }
        public string Dia { get; set; }
        public string HoraApertura { get; set; }
        public string HoraCierre { get; set; }
        public bool Estado { get; set; } = true;
        public int ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }

    }
}