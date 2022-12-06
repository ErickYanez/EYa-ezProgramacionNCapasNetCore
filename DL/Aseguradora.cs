using System;
using System.Collections.Generic;

namespace DL;

public partial class Aseguradora
{
    public int IdAseguradora { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaModificacion { get; set; }

    public byte IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }
}
