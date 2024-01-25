namespace WSVentas.Models.Response
{
    public class VentaResponse
    {                

        public string? Fecha { get; set; }     

        public string Producto { get; set; } = null!;       

        public decimal Precio { get; set; }

        public int? Cantidad { get; set; }

        public decimal? Total { get; set; }

        public string? IdTransaccion { get; set; }

    }
}
