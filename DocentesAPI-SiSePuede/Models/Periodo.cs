using System;
using System.Collections.Generic;

namespace DocentesAPI_SiSePuede.Models;

public partial class Periodo
{
    public int Id { get; set; }

    public short Año { get; set; }

    public virtual ICollection<Calificacion> Calificacion { get; set; } = new List<Calificacion>();

    public virtual ICollection<DocenteAlumno> DocenteAlumno { get; set; } = new List<DocenteAlumno>();

    public virtual ICollection<DocenteGrupo> DocenteGrupo { get; set; } = new List<DocenteGrupo>();
}
