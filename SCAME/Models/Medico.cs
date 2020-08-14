namespace SCAME.Models
{
    public class Medico
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string CodigoSenecyt { get; set; }
        public string TituloEgresado { get; set; }
        public bool Estado { get; set; } = true;

    }
}