using Models.Enum;

namespace Models.Models;

public class ItemColecao
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int LivroId { get; set; }

    public EStatusLeitura StatusLeitura { get; set; }
    public int ProgressoPrcentual { get; set; }
    public string? NotasPessoais { get; set; }
    public int Rating { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataConclusao { get; set; }
    
    
}