using System;
using System.Collections.Generic;

namespace DocentesAPI_SiSePuede.Models;

public partial class Grupo
{
    public int Id { get; set; }

    public string Grado { get; set; } = null!;

    public string Seccion { get; set; } = null!;

    public virtual ICollection<Alumno> Alumno { get; set; } = new List<Alumno>();

    public virtual ICollection<DocenteGrupo> DocenteGrupo { get; set; } = new List<DocenteGrupo>();
}
