using WSVentas.Models.Response;
using WSVentas.Models;
using WSVentas.Models.Request;

namespace WSVentas.Services
{
    public class VentaService : IVentaService
    {
        public void Add(VentaRequest model) 
        {

            StudyContext db = new StudyContext();

            using var transaction = db.Database.BeginTransaction();
            try
            {
                Venta venta = new();
                venta.TotalProducto = model.DetalleVenta.Count;
                venta.Fecha = DateTime.Now;
                venta.Contacto = model.Contacto;
                venta.Telefono = model.Telefono;
                venta.Direccion = model.Direccion;
                venta.IdUsuario = model.IdUsuario;
                venta.IdTransaccion = "code0001";
                venta.IdDistrito = model.IdDistrito;
                venta.MontoTotal = model.DetalleVenta.Sum(d => d.Total);
                db.Venta.Add(venta);
                db.SaveChanges();

                foreach (var deta in model.DetalleVenta)
                {
                    DetalleVenta detalle = new();
                    detalle.IdVenta = venta.IdVenta;
                    detalle.Cantidad = deta.Cantidad;
                    detalle.IdProducto = deta.IdProducto;
                    detalle.Total = deta.Total;
                    db.DetalleVenta.Add(detalle);
                    db.SaveChanges();
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Ocurrió un error al guardar los datos");
            }



        }
    }
}
