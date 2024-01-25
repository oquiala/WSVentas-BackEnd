using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVentas.Models.Response;
using WSVentas.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;

namespace WSVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Exito = 0;
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    List<Producto> list = db.Productos.Include(x => x.IdMarcaNavigation)
                                                      .Include(x => x.IdCategoriaNavigation)
                                                      .ToList();

                    List<ProductoResponse> listProd = new();

                    foreach (var item in list)
                    {
                        ProductoResponse prod = new();
                        
                        prod.IdProducto = item.IdProducto;                       
                        prod.Nombre = item.Nombre;
                        prod.Descripcion = item.Descripcion;
                        prod.IdMarca = item.IdMarca;
                        prod.Marca = item.IdMarcaNavigation.Descripcion;
                        prod.IdCategoria = item.IdCategoria;
                        prod.Categoria = item.IdCategoriaNavigation.Descripcion;
                        prod.Precio = item.Precio;
                        prod.Stock = item.Stock;
                        prod.RutaImagen = item.RutaImagen;
                        prod.NombreImagen = item.NombreImagen;
                        prod.Activo = item.Activo;
                        listProd.Add(prod);
                    }

                    respuesta.Exito = 1;
                    respuesta.Data = listProd;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPost]
        public IActionResult Add(Producto prod)
        {
            Respuesta resp = new Respuesta();
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    Producto oProd = new();
                    oProd.Nombre = prod.Nombre;
                    oProd.Descripcion = prod.Descripcion;
                    oProd.IdMarca = prod.IdMarca;
                    oProd.IdCategoria = prod.IdCategoria;
                    oProd.Precio = prod.Precio;
                    oProd.Stock = prod.Stock;
                    oProd.Activo = prod.Activo;
                    oProd.RutaImagen = prod.RutaImagen;
                    oProd.NombreImagen = prod.NombreImagen;
                    oProd.Fecha = DateTime.Now;

                    db.Productos.Add(oProd);
                    db.SaveChanges();
                    resp.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                resp.Mensaje = ex.Message;
            }
            return Ok(resp);
        }

        [HttpPut]
        public IActionResult Edit(Producto prod)
        {
            Respuesta resp = new Respuesta();
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    Producto oProd = db.Productos.Find(prod.IdProducto);
                    oProd.Nombre = prod.Nombre;
                    oProd.Descripcion = prod.Descripcion;
                    oProd.IdMarca = prod.IdMarca;
                    oProd.IdCategoria = prod.IdCategoria;
                    oProd.Precio = prod.Precio;
                    oProd.Stock = prod.Stock;
                    oProd.Activo = prod.Activo;
                    oProd.RutaImagen = prod.RutaImagen;
                    oProd.NombreImagen = prod.NombreImagen;

                    db.Entry(oProd).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    db.SaveChanges();
                    resp.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                resp.Mensaje = ex.Message;
            }
            return Ok(resp);
        }

        [HttpDelete]
        public IActionResult Delete(int IdProducto)
        {

            Respuesta resp = new Respuesta();
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    Producto oProd = db.Productos.Find(IdProducto);
                    db.Remove(oProd);
                    db.SaveChanges();
                    resp.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                resp.Mensaje = ex.Message;
            }
            return Ok(resp);
        }

    }
}
