using System.Text.Json;
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
builder.Services.AddScoped<NotificationService>();

builder.Services.AddScoped<JwtAuthenticationHeaderHandler>();

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.Converters.Add(new DateOnlyJsonConverter());
});


var apiUrl = builder.Configuration["ApiBaseUrl"];

builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<JwtAuthenticationHeaderHandler>();

    return new HttpClient(handler)
    {
        BaseAddress = new Uri(apiUrl)
    };
});


builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<ITokenService, TokenService>();

await builder.Build().RunAsync();
