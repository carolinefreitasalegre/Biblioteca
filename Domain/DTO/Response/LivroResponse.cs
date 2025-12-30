using System.Text.Json.Serialization;
using Models.Enum;

namespace Domain.DTO.Response;

public class LivroResponse
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string? Isbn { get; set; }
    public string? Editora { get; set; }
    public int NumeroPaginas { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public ECategoriaLivro Categoria { get; set; }
    public string? CapaUrl { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public EStatusLeitura StatusLeitura { get; set; }
    
    public DateTime DataAdicionado { get; set; } 
    public DateTime AnoPublicacao { get; set; }
    public string NotasPessoais { get; set; }

}