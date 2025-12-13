using System.Net.Http.Headers;
using Blazored.LocalStorage;


namespace Biblioteca.Client.ServiceClient;

public class JwtAuthenticationHeaderHandler : DelegatingHandler
{
   private readonly ILocalStorageService _localStorage;
   private const string TokenKey = "authToken";

   public JwtAuthenticationHeaderHandler(ILocalStorageService localStorage)
   {
      _localStorage = localStorage;
      
   }

   protected override async Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request, 
      CancellationToken cancellationToken)
   {
      var token = await _localStorage.GetItemAsync<string>(TokenKey);

      if (!string.IsNullOrEmpty(token))
      {
         request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
      }
      return await base.SendAsync(request, cancellationToken);
   }
}