using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Biblioteca.Client.ServiceClient.InterfacesClient;
using Domain.DTO;
using Domain.DTO.Response;
using Microsoft.AspNetCore.Components.Forms;
using Models.Enum;

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

    public async Task<LivroResponse> AdicionarLivros(LivroRequest model, IBrowserFile capaLivro)
    {
        using var content = new MultipartFormDataContent();

        foreach (var prop in model.GetType().GetProperties())
        {
            var value = prop.GetValue(model);
            if (value != null)
            {
                string stringValue = value is DateOnly date 
                    ? date.ToString("yyyy-MM-dd") 
                    : value.ToString()!;

                content.Add(new StringContent(stringValue), prop.Name);
            }
        }

        var fileStream = capaLivro.OpenReadStream(maxAllowedSize: 1024 * 1024 * 5);
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(capaLivro.ContentType);
    
        content.Add(fileContent, "arquivocapa", capaLivro.Name);

        var response = await _httpClient.PostAsync("api/Livro/adicionar-livro", content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<LivroResponse>(_jsonOptions);
    }

    public async Task<LivroResponse> EditarLivro(LivroRequest model)
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