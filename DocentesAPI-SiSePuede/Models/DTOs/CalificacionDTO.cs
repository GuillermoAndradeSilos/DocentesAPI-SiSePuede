namespace DocentesAPI_SiSePuede.Models.DTOs
{
    public class CalificacionDTO
    {
        public Calificacion? Calificacion { get; set; }
        public string Comentario { get; set; } = "Sin comentarios";
        public int Alumno { get; set; }
        public int Asignatura { get; set; }
    }
}
