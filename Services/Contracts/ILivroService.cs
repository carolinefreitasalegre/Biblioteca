using Domain.DTO;
using Domain.DTO.Response;
using Microsoft.AspNetCore.Http;
using Models.Models;

namespace Services.Contracts;

public interface ILivroService
{
    Task<List<LivroResponse>> Listar();
    Task<LivroResponse?> GetById(int id);
    Task<LivroResponse> Adicionar(LivroRequest livro, IFormFile? capaLivro);
    Task<LivroResponse> Atualizar(LivroRequest livro);
}