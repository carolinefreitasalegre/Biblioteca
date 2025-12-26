using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Biblioteca.Client.ServiceClient.InterfacesClient;
using Domain.DTO;
using Domain.DTO.Response;

namespace Biblioteca.Client.ServiceClient.ServicesClient;

public class UsuarioService : IUsuarioService
{
    private readonly HttpClient _httpClient;


    public UsuarioService(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"MOTIVO DO ERRO 400: {errorContent}");
        
            response.EnsureSuccessStatusCode();
        }
        
        return await response.Content.ReadFromJsonAsync<UsuarioRequest>();

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


        return await response.Content.ReadFromJsonAsync<UsuarioRequest>();
    }

    
}