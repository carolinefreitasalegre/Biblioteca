using System.Net.Http.Json;
using Biblioteca.Client.ServiceClient.InterfacesClient;
using Domain.DTO;
using Domain.DTO.Response;

namespace Biblioteca.Client.ServiceClient.ServicesClient;

public class UsuarioService : IUsuarioService
{
    private readonly HttpClient _httpClient;


    public UsuarioService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient.CreateClient("API");
    }
    
    public async Task<List<UsuarioResponse>> ListarUsuarios()
    {

        return await _httpClient.GetFromJsonAsync<List<UsuarioResponse>>("api/Usuario/usuarios");

    }

    public async Task<UsuarioResponse> GetUsuarioById(int id)
    {
        return await _httpClient.GetFromJsonAsync<UsuarioResponse>("api/Usuario/usuario/id");
    }

    public async Task<UsuarioRequest> AddUsuario(UsuarioRequest model)
    {
        var response =  await _httpClient.PostAsJsonAsync("api/Usuario/adicionar-usuario", model);
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<UsuarioRequest>();
    }

    public async Task<UsuarioRequest> UpdateUsuario(UsuarioRequest model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Usuario/editar-usuario", model);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UsuarioRequest>();
    }
}