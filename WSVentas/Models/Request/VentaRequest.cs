using System.ComponentModel.DataAnnotations;

namespace WSVentas.Models.Request
{
    public class VentaRequest
    {
        [Required]
        public int IdVenta { get; set; }

        [Required]
        [ExisteUsuario(ErrorMessage = "El usuario no existe")]
        public int IdUsuario { get; set; }         

        public string? Contacto { get; set; }       

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public string? IdDistrito { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Debe enviar los detalles de la venta")]
        public virtual List<Detalle> DetalleVenta { get; set; } = new List<Detalle>();
    }

    public class Detalle
    {  
        public int IdProducto { get; set; }

        public int? Cantidad { get; set; }

        public decimal? Total { get; set; }        
    }

    public class ExisteUsuarioAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            int idUsuario = (int)value;

            using (var db = new Models.StudyContext())
            {
                if (db.Usuarios.Find(idUsuario) == null) return false; 
            }

            return true;
        }    
    }


}
