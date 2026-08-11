using Domain.DTO;
using Domain.DTO.Response;

using Microsoft.AspNetCore.Http;

namespace Services.Contracts;

public interface ILivroService
{
    Task<List<LivroResponse>> Listar();
    Task<LivroResponse?> GetById(int id);
    Task<LivroResponse> Adicionar(LivroRequest request);
    Task<LivroResponse> Atualizar(LivroRequest request);
}