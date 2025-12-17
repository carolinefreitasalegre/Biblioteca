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

        if (string.IsNullOrWhiteSpace(jwt))
            return claims;

        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);

        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonBytes);

        if (keyValuePairs == null)
            return claims;

        string? userId = null;

        if (keyValuePairs.TryGetValue("nameid", out var nameId))
            userId = nameId.GetString();

        if (string.IsNullOrWhiteSpace(userId) &&
            keyValuePairs.TryGetValue(JwtRegisteredClaimNames.Sub, out var sub))
            userId = sub.GetString();

        if (!string.IsNullOrWhiteSpace(userId))
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

        if (keyValuePairs.TryGetValue("name", out var name))
            claims.Add(new Claim(ClaimTypes.Name, name.GetString() ?? string.Empty));

        if (keyValuePairs.TryGetValue(JwtRegisteredClaimNames.Email, out var email))
            claims.Add(new Claim(ClaimTypes.Email, email.GetString() ?? string.Empty));

        if (keyValuePairs.TryGetValue("role", out var roles))
        {
            if (roles.ValueKind == JsonValueKind.Array)
            {
                foreach (var role in roles.EnumerateArray())
                    claims.Add(new Claim(ClaimTypes.Role, role.GetString() ?? string.Empty));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, roles.GetString() ?? string.Empty));
            }
        }

        if (keyValuePairs.TryGetValue(JwtRegisteredClaimNames.Iss, out var iss))
            claims.Add(new Claim(ClaimTypes.System, iss.GetString() ?? string.Empty));

        if (keyValuePairs.TryGetValue(JwtRegisteredClaimNames.Aud, out var aud))
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, aud.GetString() ?? string.Empty));

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