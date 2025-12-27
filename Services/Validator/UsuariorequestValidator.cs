using Domain.DTO;
using FluentValidation;
using Repositories.Repositories.Contracts;

namespace Domain.Validator;

public class UsuariorequestValidator : AbstractValidator<UsuarioRequest>
{
    public UsuariorequestValidator(IUsuarioRepository  usuarioRepository)
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        // Email
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail inválido.")
            .MustAsync(async (model, email, cancellation) => {
                var existe = await usuarioRepository.EmailJaExiste(email);
                if (!existe) return true;

                var usuarioNoBanco = await usuarioRepository.ObterPorEmail(email);
                return usuarioNoBanco.Id == model.Id;
            })
            .WithMessage("Este e-mail já está em uso.");

        // Senha
        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.").When(x => x.Id == 0)
            .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.")
            .MaximumLength(50).WithMessage("A senha deve ter no máximo 50 caracteres.")
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d).+$")
            .WithMessage("A senha deve conter pelo menos uma letra e um número.");

        // Role (Enum)
        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Perfil de usuário inválido.");

        // Status (Enum)
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status do usuário inválido.");

        // CriadoEm
        RuleFor(x => x.CriadoEm)
            .NotEmpty().WithMessage("A data de criação é obrigatória.")
            .LessThanOrEqualTo(x => DateTime.UtcNow.AddMinutes(5))
            .WithMessage("A data de criação não pode ser no futuro.");
    }
}