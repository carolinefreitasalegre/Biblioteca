using Domain.DTO;
using Domain.DTO.Response;
using Models.Models;

namespace Services.Contracts;

public interface ILivroService
{
    Task<List<LivroResponse>> Listar();
    Task<LivroResponse?> GetById(int id);
    Task<LivroResponse> Adicionar(LivroRequest livro, int usuarioId);
    Task<LivroResponse> Atualizar(LivroRequest livro);
}