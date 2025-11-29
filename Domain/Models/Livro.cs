using Models.Enum;

namespace Models.Models;

public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string? Isbn { get; set; }
    public string? Editora { get; set; }
    public int NumeroPaginas { get; set; }
    public ECategoriaLivro Categoria { get; set; }
    public string? CapaUrl { get; set; }
    
    
}