using System.Net.Http.Json;
using Biblioteca.Client.ServiceClient.InterfacesClient;
using Blazored.LocalStorage;
using Domain.DTO;
using Domain.DTO.Response;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Biblioteca.Client.ServiceClient.ServicesClient;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _provider;
    private readonly NavigationManager _navigationManager;
    private const string ApiUrl = "api/Usuario/login";

    public AuthService(
        NavigationManager  navigationManager,
        HttpClient httpClient,
        ILocalStorageService localStorage,
        AuthenticationStateProvider provider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _provider = provider;
        _navigationManager = navigationManager;
    }

    public async Task<LoginResponse?> Login(LoginDto login)
    {
        var response = await _httpClient.PostAsJsonAsync(ApiUrl, login);

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (result is null || string.IsNullOrEmpty(result.Token))
            return null;

        await _localStorage.SetItemAsync("authToken", result.Token);

        // 🔔 Notifica o AuthenticationStateProvider
        if (_provider is CustomAuthenticationStateProvider customProvider)
        {
            customProvider.NotifyUserLoggedIn(result.Token);
        }

        return result;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _navigationManager.NavigateTo("/", forceLoad: true);
    }
}