using Application.Features.Users.Commands.AddEdit;
using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Validators
{
    public class AddEditUserCommandValidator : AbstractValidator<AddEditUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddEditUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(r => r.Name)
                .Must(DoesExistInDatabase)
                .WithMessage("Ya existe el usuario");

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

        private bool DoesExistInDatabase(string name)
        {
            return !_userRepository.AnyWIthName(name);
        }
    }
}
