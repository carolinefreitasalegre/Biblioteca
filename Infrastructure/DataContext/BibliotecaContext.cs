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
        modelBuilder.Entity<Usuario>().ToTable("Usuarios");
        modelBuilder.Entity<Livro>().ToTable("Livros");

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Livros)
            .WithOne(l => l.Usuario)
            .HasForeignKey(l => l.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }

}