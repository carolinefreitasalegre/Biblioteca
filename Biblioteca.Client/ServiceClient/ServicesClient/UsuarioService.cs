using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Biblioteca.Client.ServiceClient.InterfacesClient;
using Domain.DTO;
using Domain.DTO.Response;

namespace Biblioteca.Client.ServiceClient.ServicesClient;

public class UsuarioService : IUsuarioService
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions _jsonOptions =
        new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

    public UsuarioService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<UsuarioResponse>> ListarUsuarios()
    {

        var response = await _httpClient.GetAsync("api/Usuario/usuarios");

        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            throw new UnauthorizedAccessException("Usuário não é administrador.");
        }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro na API: {error}");
        }

        return await response.Content
                   .ReadFromJsonAsync<List<UsuarioResponse>>(_jsonOptions)
               ?? new List<UsuarioResponse>();

    }

    public async Task<UsuarioResponse> GetUsuarioById(int id)
    {
        var usuario = await _httpClient.GetAsync("api/Usuario/usuario/id");
        
        return await usuario.Content.ReadFromJsonAsync<UsuarioResponse>(_jsonOptions);
    }

    public async Task<UsuarioResponse> GetUsuarioByEmail(string email)
    {
        var response =  await _httpClient.GetAsync($"api/Usuario/buscar-por-email?email={email}");
        
        return await response.Content.ReadFromJsonAsync<UsuarioResponse>(_jsonOptions);
        
    }

    public async Task<UsuarioRequest> AddUsuario(UsuarioRequest model)
    {
        var response =  await _httpClient.PostAsJsonAsync("api/Usuario/adicionar-usuario", model);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }
        
        return await response.Content.ReadFromJsonAsync<UsuarioRequest>(_jsonOptions);

    }

    public async Task<UsuarioRequest> UpdateUsuario(UsuarioRequest model)
    {
        var response = await _httpClient.PutAsJsonAsync("api/Usuario/editar-usuario", model);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"MOTIVO DO ERRO 400: {errorContent}");
        
            response.EnsureSuccessStatusCode();
        }


        return await response.Content.ReadFromJsonAsync<UsuarioRequest>(_jsonOptions);
    }

    
}