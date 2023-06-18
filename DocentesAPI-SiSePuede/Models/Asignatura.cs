using System;
using System.Collections.Generic;

namespace DocentesAPI_SiSePuede.Models;

public partial class Asignatura
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int TipoAsignatura { get; set; }

    public virtual ICollection<Calificacion> Calificacion { get; set; } = new List<Calificacion>();

    public virtual ICollection<DocenteAsignatura> DocenteAsignatura { get; set; } = new List<DocenteAsignatura>();
}
