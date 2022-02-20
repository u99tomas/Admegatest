using Application.Features.Roles.Commands.Add;
using FluentValidation;

namespace Application.Validators
{
    public class AddEditRoleCommandValidator : AbstractValidator<AddEditRoleCommand>
    {
        public AddEditRoleCommandValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("El nombre es requerido");

            RuleFor(u => u.Name)
                .MaximumLength(50)
                .WithMessage("El nombre no puede exceder los 50 caracteres");

            RuleFor(u => u.Description)
                .MaximumLength(100)
                .WithMessage("La descripción no puede exceder los 100 caracteres");
        }
    }
}
