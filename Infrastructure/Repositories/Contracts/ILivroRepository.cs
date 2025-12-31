using Models.Models;

namespace Repositories.Repositories.Contracts;

public interface ILivroRepository
{
    Task<List<Livro>> Listar(int userId);
    Task<Livro?> GetById(int id);
    Task<Livro> Atualizar(Livro livro);
    Task<Livro> Adicionar(Livro livro);
}