using Application.Features.Roles.Commands.Add;
using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Validators
{
    public class AddEditRoleCommandValidator : AbstractValidator<AddEditRoleCommand>
    {
        public AddEditRoleCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("El nombre es requerido");

            RuleFor(r => r.Name)
                .MaximumLength(50)
                .WithMessage("El nombre no puede exceder los 50 caracteres");

            RuleFor(r => r.Description)
                .MaximumLength(100)
                .WithMessage("La descripción no puede exceder los 100 caracteres");
        }
    }
}
