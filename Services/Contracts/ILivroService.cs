using Domain.DTO.Response;
using Models.Models;

namespace Services.Contracts;

public interface ILivroService
{
    Task<List<Livro>> Listar();
    Task<Livro?> GetById(int id);
    Task<Livro> Adicionar(Livro livro, int usuarioId);
    Task<Livro> Atualizar(Livro livro);
}