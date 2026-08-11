using Domain.DTO;
using Domain.DTO.Response;

namespace Biblioteca.Client.ServiceClient.InterfacesClient;

public interface ILivroService
{
    Task<List<LivroResponse>> ListarLivros();
    Task<LivroResponse> AtualizarLivros(LivroRequest model);
    Task<LivroResponse> AdicionarLivros(LivroRequest model);
    Task<LivroResponse> BuscarLivroPorId(int id);
}