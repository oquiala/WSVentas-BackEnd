using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WSVentas.Models;
using WSVentas.Models.Response;
using Microsoft.AspNetCore.Authorization;
using WSVentas.Models.Request;

namespace WSVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        [Authorize]
        [HttpGet]        
        public IActionResult Get()
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Exito = 0;
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    List<Marca> list = db.Marcas.ToList();
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

        [Authorize]
        [HttpPost]
        public IActionResult Add(MarcaRequest marca)
        {
            Respuesta resp = new Respuesta();            
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    Marca oMarca = new();
                    oMarca.Descripcion = marca.Descripcion;
                    oMarca.Activo = marca.Activo;
                    oMarca.Fecha = DateTime.Now;

                    db.Marcas.Add(oMarca);
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

        [Authorize]
        [HttpPut]
        public IActionResult Edit(Marca marca)
        {
            Respuesta resp = new Respuesta();
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    Marca oMarca = db.Marcas.Find(marca.IdMarca);
                    oMarca.Descripcion = marca.Descripcion;
                    oMarca.Activo = marca.Activo;
                    
                    db.Entry(oMarca).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    
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

        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int IdMarca)
        {
            Respuesta resp = new Respuesta();
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    Marca oMarca = db.Marcas.Find(IdMarca);              
                    db.Remove(oMarca);
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
