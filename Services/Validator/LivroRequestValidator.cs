using Domain.DTO;
using FluentValidation;

namespace Domain.Validator;

public class LivroRequestValidator : AbstractValidator<LivroRequest>
{
    public LivroRequestValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("Título é obrigatório")
            .MaximumLength(150);

        RuleFor(x => x.Autor)
            .NotEmpty().WithMessage("Autor é obrigatório");

        RuleFor(x => x.NumeroPaginas)
            .GreaterThan(0);

        RuleFor(x => x.AnoPublicacao)
            .Must(data =>
                !data.HasValue || data.Value <= DateOnly.FromDateTime(DateTime.Today)
            )
            .WithMessage("Ano de publicação não pode ser maior que a data atual");



        RuleFor(x => x.Categoria)
            .IsInEnum();

        RuleFor(x => x.StatusLeitura)
            .IsInEnum();
    }
}