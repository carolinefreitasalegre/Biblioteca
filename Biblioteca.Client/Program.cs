using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Biblioteca.Client;
using Biblioteca.Client.ServiceClient.InterfacesClient;
using Biblioteca.Client.ServiceClient.ServicesClient;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddRadzenComponents();


builder.Services.AddScoped<IAuthService, AuthService>();



builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();


builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();


builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5008") 
});

await builder.Build().RunAsync();
