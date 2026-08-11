using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Biblioteca.Client;
using Biblioteca.Client.ServiceClient;
using Biblioteca.Client.ServiceClient.InterfacesClient;
using Biblioteca.Client.ServiceClient.ServicesClient;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddSingleton(new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    Converters =
    {
        new DateOnlyJsonConverter(),
        new JsonStringEnumConverter()
    }
});

var apiUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:5008/"; 

builder.Services.AddTransient<JwtAuthenticationHeaderHandler>();

builder.Services.AddHttpClient("BibliotecaAPI", client =>
{
    client.BaseAddress = new Uri(apiUrl);
})
.AddHttpMessageHandler<JwtAuthenticationHeaderHandler>(); 

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BibliotecaAPI"));

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<ITokenService, TokenService>();

await builder.Build().RunAsync();