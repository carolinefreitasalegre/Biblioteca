using Domain.DTO;
using Domain.DTO.Response;
using Models.Models;

namespace Services.Contracts;

public interface IUSuarioservice
{
    Task<List<UsuarioResponse>> ListarUsuarios();
    Task<UsuarioResponse?> ObterPorEmail(string email);
    Task<UsuarioResponse?> ObterPorId(int id);
    Task<UsuarioResponse> AdicionarUsuario(UsuarioRequest usuario);
    Task<UsuarioResponse> AtualizarUsuario(UsuarioRequest usuario);
}