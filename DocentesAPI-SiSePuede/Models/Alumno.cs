using System;
using System.Collections.Generic;

namespace DocentesAPI_SiSePuede.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Matricula { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int Edad { get; set; }

    public string Curp { get; set; } = null!;

    public double Peso { get; set; }

    public double Estatura { get; set; }

    public string? Alergico { get; set; }

    public int IdGrupo { get; set; }

    public virtual ICollection<AlumnoTutor> AlumnoTutor { get; set; } = new List<AlumnoTutor>();

    public virtual ICollection<Calificacion> Calificacion { get; set; } = new List<Calificacion>();

    public virtual ICollection<DocenteAlumno> DocenteAlumno { get; set; } = new List<DocenteAlumno>();

    public virtual Grupo IdGrupoNavigation { get; set; } = null!;

    public virtual ICollection<Notasdirector> Notasdirector { get; set; } = new List<Notasdirector>();
}
