using System;
using System.Collections.Generic;

namespace WSVentas.Models;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime Fecha { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
