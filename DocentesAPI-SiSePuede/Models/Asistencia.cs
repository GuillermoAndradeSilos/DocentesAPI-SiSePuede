using System;
using System.Collections.Generic;

namespace DocentesAPI_SiSePuede.Models;

public partial class Asistencia
{
    public int Id { get; set; }

    public int IdAlumno { get; set; }

    public DateOnly Fecha { get; set; }

    public sbyte? Asistio { get; set; }
}
