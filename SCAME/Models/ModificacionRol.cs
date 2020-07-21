namespace SCAME.Models
{
    public class ModificacionRol
    {
        public string RolName { get; set; }
        public string RolId { get; set; }
        public string[] AumentarIds { get; set; }
        public string[] QuitarIds { get; set; }

    }
}