using System;
using System.Collections.Generic;

namespace DocentesAPI_SiSePuede.Models;

public partial class Notasdirector
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int IdAlumno { get; set; }

    public virtual Alumno IdAlumnoNavigation { get; set; } = null!;
}
