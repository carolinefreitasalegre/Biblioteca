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
    
    
        
}