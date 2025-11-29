using Models.Models;
using Repositories.DataContext;
using Repositories.Repositories.Contracts;

namespace Repositories.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly BibliotecaContext _context;
    
    public UsuarioRepository(BibliotecaContext context)
    {
        _context = context;
    }
    public Task<List<Usuario>> ListarUsuarios()
    {
        return null;
    }

    public async Task<Usuario?> ObterPorEmail(string email)
    {
        return  _context.Usuarios.FirstOrDefault(x => x.Email == email);
    }

    public async Task<Usuario?> ObterPorId(int id)
    {
        return  _context.Usuarios.FirstOrDefault(x => x.Id == id);
    }

    public async Task<Usuario> AdicionarAtualizar(Usuario usuario, int id)
    { 
        await _context.Usuarios.AddAsync(usuario);
        //_context.SaveChanges();
        return usuario;
    }
}