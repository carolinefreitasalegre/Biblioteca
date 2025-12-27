using Domain.DTO;
using Domain.DTO.Response;

namespace Biblioteca.Client.ServiceClient.InterfacesClient;

public interface IAuthService
{
    Task<LoginResponse?> Login(LoginDto login);
    Task Logout();
        
}