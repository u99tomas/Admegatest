using Application.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage("Se requiere usuario");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Se requiere contraseña");
        }
    }
}
