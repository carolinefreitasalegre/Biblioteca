using Models.Models;

namespace Repositories.Repositories.Contracts;

public interface IUsuarioRepository
{
    Task<List<Usuario>> ListarUsuarios();
    Task<Usuario?> ObterPorEmail(string email);
    Task<bool> EmailJaExiste(string email);

    Task<Usuario?> ObterPorId(int id);
    Task<Usuario> Atualizar(Usuario usuario);
    Task<Usuario> Adicionar(Usuario usuario);
    
    
}