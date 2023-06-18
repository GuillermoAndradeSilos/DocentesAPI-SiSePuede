using System;
using System.Collections.Generic;

namespace DocentesAPI_SiSePuede.Models;

public partial class Tutor
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Celular { get; set; }

    public string? Email { get; set; }

    public int Idusuario { get; set; }

    public int Ocupacion { get; set; }

    public virtual ICollection<AlumnoTutor> AlumnoTutor { get; set; } = new List<AlumnoTutor>();

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
