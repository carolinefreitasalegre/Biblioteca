using Models.Models;

namespace Repositories.Repositories.Contracts;

public interface ILivroRepository
{
    Task<List<Livro>> Listar(int userId);
    Task<Livro?> GetById(int id);
    Task<Livro> Adicionar(Livro livro);
    Task<Livro> Atualizar(Livro livro);
}