using WSVentas.Models.Request;
using WSVentas.Models.Response;

namespace WSVentas.Services
{
    public interface IUserService
    {
        UserResponse? Auth(AuthRequest model);

    }
}
