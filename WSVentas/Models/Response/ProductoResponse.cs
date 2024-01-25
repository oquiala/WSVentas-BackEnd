namespace WSVentas.Models.Response
{
    public class ProductoResponse
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int IdMarca { get; set; }

        public string? Marca { get; set; }

        public int IdCategoria { get; set; }

        public string? Categoria { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string? RutaImagen { get; set; }

        public string? NombreImagen { get; set; }

        public bool? Activo { get; set; }
        
    }
}
