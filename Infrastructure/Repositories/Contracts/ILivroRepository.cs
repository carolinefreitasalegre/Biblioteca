using Models.Models;

namespace Repositories.Repositories.Contracts;

public interface ILivroRepository
{
    Task<List<Livro>> Listar();
    Task<Livro?> GetById(int id);
    Task<Livro> AdicionarAtualizar(Livro livro, int id);
}