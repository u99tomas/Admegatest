using Application.Features.Roles.Commands.Add;
using FluentValidation;

namespace Application.Validators
{
    public class AddRoleCommandValidator : Validator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("El nombre es requerido");

            RuleFor(u => u.Name)
                .MinimumLength(5)
                .WithMessage("Ingresa al menos 5 caracteres");

            RuleFor(u => u.Name)
                .MaximumLength(50)
                .WithMessage("El nombre no puede exceder los 50 caracteres");

            RuleFor(u => u.Description)
                .NotEmpty()
                .WithMessage("La descripción es requerida");

            RuleFor(u => u.Description)
                .MinimumLength(10)
                .WithMessage("Ingresa al menos 10 caracteres");

            RuleFor(u => u.Description)
                .MaximumLength(100)
                .WithMessage("La descripción no puede exceder los 100 caracteres");
        }
    }
}
