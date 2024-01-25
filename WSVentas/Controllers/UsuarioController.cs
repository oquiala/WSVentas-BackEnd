using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVentas.Models;
using WSVentas.Models.Request;
using WSVentas.Models.Response;
using WSVentas.Services;

namespace WSVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUserService _userService;

        public UsuarioController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model)
        {
            Respuesta respuesta = new();
            var userresponse = _userService.Auth(model);
            if (userresponse == null)
            {
                respuesta.Mensaje = "Credenciales incorrectas";
                respuesta.Exito = 0;
                return Ok(respuesta);
            }
            respuesta.Exito = 1;
            respuesta.Data = userresponse;
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
                    List<Usuario> list = db.Usuarios.ToList();
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
        public IActionResult Add(Usuario user)
        {
            Respuesta resp = new Respuesta();
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    Usuario oUser = new Usuario();
                    oUser.Usuario1 = user.Usuario1;
                    oUser.Nombre = user.Nombre;                    
                    oUser.Apellidos = user.Apellidos;
                    oUser.Correo = user.Correo;                   
                    oUser.Clave = user.Clave;
                    oUser.Activo = user.Activo;
                    oUser.Fecha = DateTime.Now;
                    oUser.Reestablecer = false;

                    db.Usuarios.Add(oUser);
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
        public IActionResult Edit(Usuario user)
        {
            Respuesta resp = new Respuesta();
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    Usuario oUser = db.Usuarios.Find(user.IdUsuario);
                    oUser.Nombre = user.Nombre;
                    oUser.Activo = user.Activo;

                    db.Entry(oUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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
        public IActionResult Delete(int IdUser)
        {
            Respuesta resp = new Respuesta();
            try
            {
                using (StudyContext db = new StudyContext())
                {
                    Usuario oUser = db.Usuarios.Find(IdUser);
                    db.Remove(oUser);
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
