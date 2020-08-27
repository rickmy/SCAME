using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.Models
{
    public class Sexo
    {
        [Key]
        public int IdSexo { get; set; }
        public string NombreSexo { get; set; }
        public bool Estado { get; set; }
    }
}
