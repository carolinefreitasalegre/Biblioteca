using Domain.DTO;
using FluentValidation;
using Repositories.Repositories.Contracts;

namespace Domain.Validator;

public class UsuarioUpdateValidator : AbstractValidator<UsuarioRequest>
{
    public UsuarioUpdateValidator(IUsuarioRepository usuarioRepository)
    {
       

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail inválido.")
            .MustAsync(async (model, email, cancellation) => {
                var usuarioNoBanco = await usuarioRepository.ObterPorEmail(email);
                
                if (usuarioNoBanco == null) return true;

                return usuarioNoBanco.Id == model.Id;
            })
            .WithMessage("Este e-mail já está em uso por outro usuário.");

        RuleFor(x => x.Senha)
            .MinimumLength(6).WithMessage("A nova senha deve ter no mínimo 6 caracteres.")
            .MaximumLength(50).WithMessage("A nova senha deve ter no máximo 50 caracteres.")
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d).+$")
            .WithMessage("A senha deve conter pelo menos uma letra e um número.")
            .When(x => !string.IsNullOrWhiteSpace(x.Senha)); 

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Perfil de usuário inválido.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status do usuário inválido.");
            
      
    }
}