using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSVentas.Models;
using WSVentas.Models.Request;
using WSVentas.Models.Response;
using WSVentas.Services;

namespace WSVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpPost]
        public IActionResult AddVenta(VentaRequest model )
        {
            Respuesta respuesta = new();

            try
            {               
                _ventaService.Add(model);
                respuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
                respuesta.Exito = 0;
                return Ok(respuesta);
            }
            
            return Ok(respuesta);
        }

        [HttpGet]
        public IActionResult Get()
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Exito = 0;
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    List<DetalleVenta> lista = db.DetalleVenta .Include(x => x.IdProductoNavigation)
                                                               .Include(x => x.IdVentaNavigation)
                                                               .ToList();
                    List<VentaResponse> listVenta = new();

                    foreach (var item in lista)
                    {
                        VentaResponse venta = new VentaResponse();                       
                        venta.Fecha = item.IdVentaNavigation.Fecha.ToString("dd/MM/yyyy");                   
                        venta.Producto = item.IdProductoNavigation.Nombre;
                        venta.Precio = item.IdProductoNavigation.Precio;
                        venta.Cantidad = item.Cantidad;
                        venta.Total = item.Total;
                        venta.IdTransaccion = item.IdVentaNavigation.IdTransaccion;
                        listVenta.Add(venta);
                    }

                    respuesta.Exito = 1;
                    respuesta.Data = listVenta;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }



    }
}
