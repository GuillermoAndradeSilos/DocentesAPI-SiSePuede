using System;
using System.Collections.Generic;

namespace DocentesAPI_SiSePuede.Models;

public partial class DocenteAsignatura
{
    public int Id { get; set; }

    public int IdDocente { get; set; }

    public int IdAsignatura { get; set; }

    public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;

    public virtual Docente IdDocenteNavigation { get; set; } = null!;
}
