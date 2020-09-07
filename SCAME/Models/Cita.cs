﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Models
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime FechaCita { get; set; }
        public string MedicoId { get; set; }
        public Medico Medico { get; set; }
        public string MotivoCita { get; set; }
        public string MotivoAnulacion { get; set; }
        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }
        public string ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }

    }
}
