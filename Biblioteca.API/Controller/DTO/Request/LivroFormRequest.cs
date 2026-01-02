using Models.Enum;

namespace Domain.DTO;

public class LivroFormRequest
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Editora { get; set; }
    public string Isbn { get; set; }

    public int NumeroPaginas { get; set; }

    public ECategoriaLivro Categoria { get; set; }
    public EStatusLeitura StatusLeitura { get; set; }

    public IFormFile ArquivoCapa { get; set; }
}