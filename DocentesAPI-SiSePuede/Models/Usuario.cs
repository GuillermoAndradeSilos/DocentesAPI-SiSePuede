using System;
using System.Collections.Generic;

namespace DocentesAPI_SiSePuede.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public int Rol { get; set; }

    public virtual ICollection<Director> Director { get; set; } = new List<Director>();

    public virtual ICollection<Docente> Docente { get; set; } = new List<Docente>();

    public virtual ICollection<Tutor> Tutor { get; set; } = new List<Tutor>();
}
