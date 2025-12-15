using System.Net.Http.Json;
using Biblioteca.Client.ServiceClient.InterfacesClient;
using Domain.DTO;
using Domain.DTO.Response;

namespace Biblioteca.Client.ServiceClient.ServicesClient;

public class LivroService : ILivroService
{
    private readonly HttpClient _httpClient;

    public LivroService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient.CreateClient("API");
    }
    
    public async Task<List<LivroResponse>> ListarLivros()
    {
        return await _httpClient.GetFromJsonAsync<List<LivroResponse>>("api/Livro/livros");
    }

    public async Task<LivroRequest> AdicionarLivros(LivroRequest model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Livro/adicionar-livro", model);

        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<LivroRequest>();
    }

    public async Task<LivroRequest> EditarLivro(LivroRequest model)
    {
        var response = await _httpClient.PutAsJsonAsync("api/Livro/editar-livro", model);
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<LivroRequest>();
    }

    public async Task<LivroResponse> BuscarLivroPorId(int id)
    {
        return await _httpClient.GetFromJsonAsync<LivroResponse>("api/Livro/livros/id");
    }

    public async Task<LivroResponse> BuscarLivroPorNome(string nome)
    {
        return await _httpClient.GetFromJsonAsync<LivroResponse>("api/Livro/livros/nome");
        
        //ajustar para criar a rota no backend
    }
}