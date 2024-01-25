using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVentas.Models.Response;
using WSVentas.Models;

namespace WSVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
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
                    List<Categoria> list = db.Categoria.ToList();
                    respuesta.Exito = 1;
                    respuesta.Data = list;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }

        [HttpPost]
        public IActionResult Add(Categoria categoria)
        {
            Respuesta resp = new Respuesta();
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    Categoria oCategoria = new Categoria();
                    oCategoria.Descripcion = categoria.Descripcion;
                    oCategoria.Activo = categoria.Activo;
                    oCategoria.Fecha = DateTime.Now;

                    db.Categoria.Add(oCategoria);
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
        public IActionResult Edit(Categoria categoria)
        {
            Respuesta resp = new Respuesta();
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    Categoria oCategoria = db.Categoria.Find(categoria.IdCategoria);
                    oCategoria.Descripcion = categoria.Descripcion;
                    oCategoria.Activo = categoria.Activo;

                    db.Entry(oCategoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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
        public IActionResult Delete(int IdCategoria)
        {
            Respuesta resp = new Respuesta();
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    Categoria oCategoria = db.Categoria.Find(IdCategoria);
                    db.Remove(oCategoria);
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
