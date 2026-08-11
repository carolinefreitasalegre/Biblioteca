using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Biblioteca.Client.ServiceClient.InterfacesClient;
using Domain.DTO;
using Domain.DTO.Response;


namespace Biblioteca.Client.ServiceClient.ServicesClient;

public class LivroService : ILivroService
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new JsonStringEnumConverter(),
            new DateOnlyJsonConverter()
        }
    };


    public LivroService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<LivroResponse>> ListarLivros()
    {
        return await _httpClient.GetFromJsonAsync<List<LivroResponse>>("api/Livro/livros", _jsonOptions);
    }

  
    public async Task<LivroResponse> AdicionarLivros(LivroRequest model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Livro/adicionar-livro", model, _jsonOptions);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<LivroResponse>(_jsonOptions);
    }
    
    
    public async Task<LivroResponse> AtualizarLivros(LivroRequest model)
    {
        var response = await _httpClient.PutAsJsonAsync("api/Livro/editar-livro", model);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<LivroResponse>(_jsonOptions);
    }

    public async Task<LivroResponse> BuscarLivroPorId(int id)
    {
        return await _httpClient.GetFromJsonAsync<LivroResponse>($"api/Livro/livro/{id}", _jsonOptions);
    }

 
}