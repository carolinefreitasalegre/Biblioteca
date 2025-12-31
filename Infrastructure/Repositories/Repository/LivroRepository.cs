using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repositories.DataContext;
using Repositories.Repositories.Contracts;

namespace Repositories.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly BibliotecaContext _context;
    
    public LivroRepository(BibliotecaContext context)
    {
        _context = context;
    }

    public async Task<List<Livro>> Listar(int userId)
    {
        return await _context.Livros.Where(u => u.UsuarioId == userId)
            .ToListAsync();
    }

    public async Task<Livro?> GetById(int id)
    {
        return await _context.Livros.FirstOrDefaultAsync(livro => livro.Id == id);
    }

    public async Task<Livro> Atualizar(Livro livro)
    {
        _context.Livros.Update(livro);
         await _context.SaveChangesAsync();
         return livro;

    }

    public async Task<Livro> Adicionar(Livro livro)
    {
        _context.Livros.Add(livro);
        await _context.SaveChangesAsync();
        return livro;
    }


}