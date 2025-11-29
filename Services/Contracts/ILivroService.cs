using Models.Models;

namespace Services.Contracts;

public interface ILivroService
{
    Task<List<Livro>> Listar();
    Task<Livro?> GetById(int id);
    Task<Livro> AdicionarAtualizar(Livro livro, int id);
}