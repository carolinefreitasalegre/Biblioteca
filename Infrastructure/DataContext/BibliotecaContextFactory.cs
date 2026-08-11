using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Repositories.DataContext;

public class BibliotecaContextFactory
    : IDesignTimeDbContextFactory<BibliotecaContext>
{
    public BibliotecaContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<BibliotecaContext>();

        options.UseSqlServer(
            "Server=localhost,1433;Database=Biblioteca;User Id=sa;Password=282328#Abc123!;TrustServerCertificate=True;");

        return new BibliotecaContext(options.Options);
    }
}