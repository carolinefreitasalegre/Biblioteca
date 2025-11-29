using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Repositories.DataContext;

public class BibliotecaContextFactory : IDesignTimeDbContextFactory<BibliotecaContext>
{
    public BibliotecaContext CreateDbContext(string[] args)
    {
        const string connectionString = "Host=localhost;Port=5432;Database=Biblioteca;Username=postgres;Password=233251";

        var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContext>();
        optionsBuilder.UseNpgsql(connectionString); 
        
        return new BibliotecaContext(optionsBuilder.Options);
    }
}