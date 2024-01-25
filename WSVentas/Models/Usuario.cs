using System;
using System.Collections.Generic;

namespace WSVentas.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public string Clave { get; set; } = null!;

    public string? Correo { get; set; }

    public bool Reestablecer { get; set; }

    public bool? Activo { get; set; }

    public DateTime Fecha { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
