using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Models
{
    public class ConsultorioHorario
    {
        public int IdConsultorio { get; set; }
        public Consultorio Consultorio { get; set; }
        public int IdHorario { get; set; }
        public Horario Horario { get; set; }
    }
}
