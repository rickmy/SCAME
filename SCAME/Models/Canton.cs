namespace SCAME.Models
{
    public class Canton
    {
        public int Id { get; set; }
        public string NombreCanton { get; set; }
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }
        public bool Estado { get; set; } = true;
    }
}