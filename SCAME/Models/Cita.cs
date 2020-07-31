using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Models
{
    public class Cita
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Dia { get; set; }
        public Horario Horario { get; set; }
        public string PrimerNombre { get; set; }
        public string PrimerApellido { get; set; }
        public Especialista Especialista { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Paciente Paciente { get; set; }
        public string Descripcion { get; set; }
        public string ModalidadEscojida { get; set; }
        public Modalidad Modalidad { get; set; }
        public string NombreConsultorio { get; set; }
        public Consultorio Consultorio { get; set; }

    }
}
