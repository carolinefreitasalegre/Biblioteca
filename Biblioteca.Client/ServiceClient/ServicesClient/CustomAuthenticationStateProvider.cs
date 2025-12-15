using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq; 


namespace Biblioteca.Client.ServiceClient.ServicesClient;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly ClaimsPrincipal _userAnonimo;

    public CustomAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        
        
        _userAnonimo = new ClaimsPrincipal(new ClaimsIdentity());
    }
    
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        
        var token = await _localStorage.GetItemAsStringAsync("authToken");
        
        if(string.IsNullOrEmpty(token))
            return new AuthenticationState(_userAnonimo);


        try
        {
            var claims = ParseClaimsFromJson(token);
            var indentity = new ClaimsIdentity(claims, "Biblioteca");
        
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
            return new AuthenticationState(new ClaimsPrincipal(indentity));
        }
        catch (Exception e)
        {
            await _localStorage.RemoveItemAsync("authToken");
            return new AuthenticationState(_userAnonimo);
        }
        
    }

    public void NotifyUserLoggedIn(string token)
    {
        var claims = ParseClaimsFromJson(token);
        var  indentity = new ClaimsIdentity(claims, "Biblioteca");
        var user = new ClaimsPrincipal(indentity);

        NotifyAuthenticationStateChanged(Task.FromResult((new AuthenticationState(user))));
    }


    public void NotifyUserLoggedOut()
    {
        _localStorage.RemoveItemAsync("authToken");
        _httpClient.DefaultRequestHeaders.Authorization = null;

        NotifyAuthenticationStateChanged(Task.FromResult((new AuthenticationState(_userAnonimo))));
    }
    
   private IEnumerable<Claim> ParseClaimsFromJson(string jwt)
    {
        var claims = new List<Claim>();

        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs == null) return claims;


        if (keyValuePairs.TryGetValue("role", out var roleValue))
        {
            if (roleValue != null)
            {
                if (roleValue is JsonElement element && element.ValueKind == JsonValueKind.Array)
                {
                    claims.AddRange(
                        element.EnumerateArray()
                            .Select(x => new Claim(ClaimTypes.Role, x.ToString()))
                    );
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleValue.ToString()!));
                }
            }
        }
        
        if (keyValuePairs.TryGetValue(JwtRegisteredClaimNames.Email, out var emailValue))
        {
            claims.Add(new Claim(ClaimTypes.Email, emailValue.ToString() ?? string.Empty));
        }

        if (keyValuePairs.TryGetValue("name", out var nameValue))
        {
            claims.Add(new Claim(ClaimTypes.Name, nameValue.ToString() ?? string.Empty));
        }

        if (keyValuePairs.TryGetValue(JwtRegisteredClaimNames.Iss, out var issValue))
        {
            claims.Add(new Claim(ClaimTypes.System, issValue.ToString() ?? string.Empty));
        }
        
        if (keyValuePairs.TryGetValue(JwtRegisteredClaimNames.Aud, out var audValue))
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audValue.ToString() ?? string.Empty));
        }
        

        return claims;
    }
    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
    
    
    
}