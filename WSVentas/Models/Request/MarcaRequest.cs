﻿
namespace WSVentas.Models.Request
{
    public class MarcaRequest
    {
        public int IdMarca { get; set; }

        public string? Descripcion { get; set; }

        public bool? Activo { get; set; }
    }
}
