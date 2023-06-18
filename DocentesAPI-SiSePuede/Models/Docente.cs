using System;
using System.Collections.Generic;

namespace DocentesAPI_SiSePuede.Models;

public partial class Docente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public int Edad { get; set; }

    public int TipoDocente { get; set; }

    public int IdUsuario { get; set; }

    public virtual ICollection<Calificacion> Calificacion { get; set; } = new List<Calificacion>();

    public virtual ICollection<DocenteAlumno> DocenteAlumno { get; set; } = new List<DocenteAlumno>();

    public virtual ICollection<DocenteAsignatura> DocenteAsignatura { get; set; } = new List<DocenteAsignatura>();

    public virtual ICollection<DocenteGrupo> DocenteGrupo { get; set; } = new List<DocenteGrupo>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
