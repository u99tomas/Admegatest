using Application.Features.Users.Commands.AddEdit;
using FluentValidation;

namespace Application.Validators
{
    public class AddEditUserCommandValidator : Validator<AddEditUserCommand>
    {
        public AddEditUserCommandValidator()
        {
            RuleFor(u => u.AccountName)
                .NotEmpty()
                .WithMessage("El nombre de la cuenta es requerido");

            RuleFor(u => u.AccountName)
                .MaximumLength(50)
                .WithMessage("El nombre de la cuenta no puede exceder los 50 caracteres");

            // password --> required, secured
            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("La contraseña es requerida");
        }
    }
}
