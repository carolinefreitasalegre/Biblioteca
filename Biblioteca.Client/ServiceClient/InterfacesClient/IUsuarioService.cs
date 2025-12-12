using Domain.DTO;
using Domain.DTO.Response;
using Models.Models;

namespace Biblioteca.Client.ServiceClient.InterfacesClient;

public interface IUsuarioService
{
    Task<List<UsuarioResponse>> ListarUsuarios();
    Task<UsuarioResponse> GetUsuarioById(int id);
    Task<UsuarioRequest>AddUsuario(UsuarioRequest model);
    
    
}