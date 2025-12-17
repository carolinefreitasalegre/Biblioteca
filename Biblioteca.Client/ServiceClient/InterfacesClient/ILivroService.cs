using Domain.DTO;
using Domain.DTO.Response;

namespace Biblioteca.Client.ServiceClient.InterfacesClient;

public interface ILivroService
{
    Task<List<LivroResponse>> ListarLivros();
    Task<LivroRequest> AdicionarLivros(LivroRequest model);
    Task<LivroRequest> EditarLivro(LivroRequest model);
    Task<LivroResponse> BuscarLivroPorId(int id);
    Task<LivroResponse> BuscarLivroPorNome(string nome);
}