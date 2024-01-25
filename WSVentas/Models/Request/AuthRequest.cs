using System.ComponentModel.DataAnnotations;

namespace WSVentas.Models.Request
{
    public class AuthRequest
    {
        [Required]
        public string? Correo { get; set; }
        [Required]
        public string Clave { get; set; } = null!;
    }
}
