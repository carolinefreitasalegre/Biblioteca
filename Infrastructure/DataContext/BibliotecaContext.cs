using Models.Models;
using Microsoft.EntityFrameworkCore;


namespace Repositories.DataContext;

public class BibliotecaContext : DbContext
{
    public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
    : base(options){}
   
    
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Livro> Livros { get; set; }
    public DbSet<ItemColecao>  ItemColecao { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Usuario>()
            .Property(u => u.Status)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }

}