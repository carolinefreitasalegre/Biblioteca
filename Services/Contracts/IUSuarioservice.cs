using Models.Models;

namespace Services.Contracts;

public interface IUSuarioservice
{
    Task<List<Usuario>> ListarUsuarios();
    Task<Usuario?> ObterPorEmail(string email);
    Task<Usuario?> ObterPorId(int id);
    Task<Usuario> AdicionarUsuario(Usuario usuario);
    Task<Usuario> AtualizarUsuario(Usuario usuario);
}