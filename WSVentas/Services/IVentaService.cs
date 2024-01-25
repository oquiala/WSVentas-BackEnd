using WSVentas.Models.Request;

namespace WSVentas.Services
{
    public interface IVentaService
    {
        public void Add(VentaRequest model);
    }
}
