using Application.Features.Users.Commands.AddEdit;
using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Validators
{
    public class AddEditUserCommandValidator : AbstractValidator<AddEditUserCommand>
    {
        public AddEditUserCommandValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("El nombre de usuario es requerido");

            RuleFor(u => u.Name)
                .MaximumLength(50)
                .WithMessage("El nombre de usuario no puede exceder los 50 caracteres");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("La contraseña es requerida");

        }
    }
}
