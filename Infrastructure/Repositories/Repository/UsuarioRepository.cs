using Microsoft.EntityFrameworkCore;
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
    public async Task<List<Usuario>> ListarUsuarios()
    {
        return await _context.Usuarios.Include(l => l.Livros).ToListAsync();
        
    }

    public async Task<Usuario?> ObterPorEmail(string email)
    {
        
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario?> ObterPorId(int id)
    {
        return  _context.Usuarios.FirstOrDefault(x => x.Id == id);
    }

    public async Task<Usuario> Atualizar(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario> Adicionar(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }
    
    public async Task<bool> EmailJaExiste(string email)
    {
        return await _context.Usuarios
            .AnyAsync(u => u.Email == email);
    }
}