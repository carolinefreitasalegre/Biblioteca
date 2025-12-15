using Biblioteca.Client.ServiceClient.InterfacesClient;
using Blazored.LocalStorage;
using Domain.DTO.Response;

namespace Biblioteca.Client.ServiceClient.ServicesClient;

public class TokenService : ITokenService
{
    private readonly ILocalStorageService _localStorage;

    public TokenService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;

    }
    
    public async Task<string> GetTokenAsync()
    {
        
        return await _localStorage.GetItemAsStringAsync("authToken");
    }
}