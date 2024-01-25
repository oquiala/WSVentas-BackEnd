using System;
using System.Collections.Generic;

namespace WSVentas.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int IdUsuario { get; set; }

    public int? TotalProducto { get; set; }

    public decimal? MontoTotal { get; set; }

    public string? Contacto { get; set; }

    public string? IdDistrito { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? IdTransaccion { get; set; }

    public DateTime Fecha { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
