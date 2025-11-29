using Models.Models;

namespace Repositories.Repositories.Contracts;

public interface IUsuarioRepository
{
    Task<List<Usuario>> ListarUsuarios();
    Task<Usuario?> ObterPorEmail(string email);
    Task<Usuario?> ObterPorId(int id);
    Task<Usuario> AdicionarAtualizar(Usuario usuario, int id);
    
}