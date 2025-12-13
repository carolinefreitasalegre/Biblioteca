using System.Net.Http.Json;
using Biblioteca.Client.ServiceClient.InterfacesClient;
using Blazored.LocalStorage;
using Domain.DTO;
using Domain.DTO.Response;

namespace Biblioteca.Client.ServiceClient.ServicesClient;

public class AuthService : IAuthService
{
    
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private const string ApiUrl = "api/Usuario/login";

    public AuthService(IHttpClientFactory httpClient, ILocalStorageService localStorage)
    {
        _httpClient =  httpClient.CreateClient("API");
        _localStorage = localStorage;
    }
    
    public async Task<LoginResponse?> Login(LoginDto login)
    {
        
        var response = await _httpClient.PostAsJsonAsync(ApiUrl, login);

        if (!response.IsSuccessStatusCode)
            return null;
        
        
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            
            await _localStorage.SetItemAsync("authToken", result.Token);

            return result;

    }
}