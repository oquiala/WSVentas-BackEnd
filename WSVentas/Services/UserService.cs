using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WSVentas.Models;
using WSVentas.Models.Common;
using WSVentas.Models.Request;
using WSVentas.Models.Response;
using WSVentas.Tools;

namespace WSVentas.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _config;

        public UserService(IOptions<AppSettings> appSettings, IConfiguration config)
        {
            _appSettings = appSettings.Value;
            _config = config;
        }

        public UserResponse? Auth(AuthRequest model)
        {
            UserResponse? response = new();
            using (var db = new StudyContext())
            {
                string clave = Encrypt.GetSHA256(model.Clave);
                var usuario = db.Usuarios.Where(d => d.Correo == model.Correo && d.Clave == clave).FirstOrDefault();

                if (usuario == null) return null;
                
                response.Correo = usuario.Correo;
                response.Token = GenerarToken(model);
            }
            return response;
        }

       

        public string GenerarToken(AuthRequest model)
        {
            var claims = new[] { new Claim(ClaimTypes.Role, model.Correo) };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: _config.GetSection("AppSettings:Secreto").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var securityToken = new JwtSecurityToken(claims: claims,
                                                     expires: DateTime.Now.AddMinutes(120),//2 horas
                                                     signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }





    }
}
