using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;
using Services.Services;
using FluentValidation;

namespace Services.DI;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services){
       
        AddServices(services);
        AddRepositories(services);
        AddValidators(services);

    }

    private static void AddServices(this IServiceCollection services){
        
        services.AddScoped<ILoggedinUser, LoggedinUser>();
        
        services.AddScoped<IUploadPhotoService, UploadPhotoService>();
        services.AddScoped<IUSuarioservice, UsuarioService>();
        services.AddScoped<ILivroService, LivroService>();
        services.AddScoped<IAuthService, AuthService>();

    }

    private static void AddValidators(this IServiceCollection service){
       
        services.AddScoped<IValidator<UsuarioRequest>, UsuariorequestValidator>();
        services.AddScoped<IValidator<UsuarioUpdateRequest>, UsuarioUpdateValidator>();

    }
}