using Domain.DTO;
using FluentValidation;

namespace Domain.Validator;

public class EditarLivroRequestValidator : AbstractValidator<LivroRequest>
{
    public EditarLivroRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id do livro é obrigatório para edição");

        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage("Título é obrigatório")
            .MaximumLength(150);

        RuleFor(x => x.Autor)
            .NotEmpty()
            .WithMessage("Autor é obrigatório");

        RuleFor(x => x.NumeroPaginas)
            .GreaterThan(0)
            .WithMessage("Número de páginas deve ser maior que zero");

        RuleFor(x => x.AnoPublicacao)
            .Must(data =>
                !data.HasValue || data.Value <= DateOnly.FromDateTime(DateTime.Today)
            )
            .WithMessage("Ano de publicação não pode ser maior que a data atual");

        RuleFor(x => x.Categoria)
            .IsInEnum()
            .WithMessage("Categoria inválida");

        RuleFor(x => x.StatusLeitura)
            .IsInEnum()
            .WithMessage("Status de leitura inválido");
    }
}