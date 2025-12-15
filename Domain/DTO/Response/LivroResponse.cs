using Models.Enum;
using Models.Models;

namespace Domain.DTO.Response;

public class LivroResponse
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string? Isbn { get; set; }
    public string? Editora { get; set; }
    public int NumeroPaginas { get; set; }
    public ECategoriaLivro Categoria { get; set; }
    public string? CapaUrl { get; set; }
    public EStatusLeitura StatusLeitura { get; set; }
    public DateTime AnoPublicacao { get; set; }
    public string NotasPessoais { get; set; }

}