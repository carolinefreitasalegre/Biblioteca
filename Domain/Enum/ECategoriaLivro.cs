using System.ComponentModel.DataAnnotations;

namespace Models.Enum;

public enum ECategoriaLivro
{
    [Display(Name = "Não especificado")]
    Nao_Especificado,

    [Display(Name = "Ficção")]
    Ficcao,

    [Display(Name = "Programação")]
    Programacao,

    [Display(Name = "História")]
    Historia,

    [Display(Name = "Romance")]
    Romance,

    [Display(Name = "Ficção científica")]
    Ficcao_Cientifica,

    [Display(Name = "Thriller")]
    Thriller
}
