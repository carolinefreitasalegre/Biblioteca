
using Repositories.DataContext;
using Repositories.Repositories;
using Repositories.Repositories.Contracts;


namespace Repositories.DI;

public static class DependencyInjection{

    public static void AddRepositories(this IServiceCollection services){
       
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ILivroRepository, LivroRepository>();

    }

    private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration config){

        var conneectionString = config.GetConnectionString();

        services.AddDbContext<BibliotecaContext>(opt => 
        {
            opt.UseSqlServer(conneectionString);
        });
    }

}






/*
    private static void AddRepositories(this IServiceCollection services){

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ILivroRepository, LivroRepository>();

    }
*/