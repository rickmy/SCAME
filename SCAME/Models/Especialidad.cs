using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Models
{
    public class Especialidad
    {
        public int Id { get; set; }
        public string NombreEspecialidad { get; set; }
        public MedicoDetalle MedicoDetalle { get; set; }
        public bool Estado { get; set; } = true;
    }
}
