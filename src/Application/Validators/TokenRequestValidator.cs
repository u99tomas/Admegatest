using Application.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator()
        {
            RuleFor(u => u.AccountName)
                .NotEmpty()
                .WithMessage("Se requiere nombre de cuenta");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Se requiere contraseña");
        }
    }
}
