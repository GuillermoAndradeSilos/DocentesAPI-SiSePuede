namespace DocentesAPI_SiSePuede.Models.DTOs
{
    public class ReporteDTO
    {
        public string Nombre { get; set; } = null!;
        public string Asignatura { get; set; } = null!;
        public Calificacion? Calificacion { get; set; }
    }
}
