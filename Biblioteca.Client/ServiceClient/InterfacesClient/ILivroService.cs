using Domain.DTO;
using Domain.DTO.Response;
using Microsoft.AspNetCore.Components.Forms;

namespace Biblioteca.Client.ServiceClient.InterfacesClient;

public interface ILivroService
{
    Task<List<LivroResponse>> ListarLivros();
    Task<LivroResponse> AdicionarLivros(LivroRequest model, IBrowserFile file);
    Task<LivroResponse> EditarLivro(LivroRequest model);
    Task<LivroResponse> BuscarLivroPorId(int id);
}