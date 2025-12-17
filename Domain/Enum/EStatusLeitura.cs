using System.ComponentModel.DataAnnotations;

namespace Models.Enum;

public enum EStatusLeitura
{
    [Display(Name = "Lido")]
    Lido = 1,

    [Display(Name = "Lendo")]
    Lendo = 2,

    [Display(Name = "Para ler")]
    Para_Ler = 3
}
