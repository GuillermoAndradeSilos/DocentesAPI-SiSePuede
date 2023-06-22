namespace DocentesAPI_SiSePuede.Models.DTOs
{
    public class TutorDTO
    {
        public Tutor Tutor { get; set; } = null!;
        public Usuario? Usuario { get; set; }
        public int Alumno { get; set; }
        public string? curp { get; set; }
    }
}
