using System.Net.Http.Json;
using Biblioteca.Client.ServiceClient.InterfacesClient;
using Domain.DTO;
using Domain.DTO.Response;

namespace Biblioteca.Client.ServiceClient.ServicesClient;

public class UsuarioService : IUsuarioService
{
    private readonly HttpClient _httpClient;
    private readonly string ApiUrl = "api/Usuario/adicionar-usuario";


    public async Task<List<UsuarioResponse>> ListarUsuarios()
    {
        var response = await _httpClient.GetAsync(ApiUrl);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<UsuarioResponse>>();

        return null;
    }

    public Task<UsuarioResponse> GetUsuarioById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioRequest> AddUsuario(UsuarioRequest model)
    {
        throw new NotImplementedException();
    }
}