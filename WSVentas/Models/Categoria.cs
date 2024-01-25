using System;
using System.Collections.Generic;

namespace WSVentas.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime Fecha { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
