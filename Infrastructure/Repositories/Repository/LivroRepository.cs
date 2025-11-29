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

    public async Task<List<Livro>> Listar()
    {
        throw new NotImplementedException();
    }

    public async Task<Livro?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Livro> AdicionarAtualizar(Livro livro, int id)
    {
        throw new NotImplementedException();
    }
}