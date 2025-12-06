using System.Net.Http.Json;
using Biblioteca.Client.ServiceClient.InterfacesClient;
using Domain.DTO;
using Domain.DTO.Response;

namespace Biblioteca.Client.ServiceClient.ServicesClient;

public class AuthService : IAuthService
{
    
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "api/Usuario/login";

    public AuthService(HttpClient httpClient)
    {
        _httpClient =  httpClient;
    }
    
    public async Task<LoginResponse?> Login(LoginDto login)
    {
        // chamar o endpoint da api do login em uma variavel httpclient.postasjson
        // Retornar a resposta (que deve conter o token, Role...)
        
        var response = await _httpClient.PostAsJsonAsync(ApiUrl, login);
        
        if(response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<LoginResponse>();

        return null;
        
        
        
        
        
        throw new NotImplementedException();
    }
}