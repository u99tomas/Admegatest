using AdMegasoft.Application.Models;
using FluentValidation;

namespace AdMegasoft.Application.Validators
{
    public class UnauthorizedUserModelValidator : AbstractValidator<UnauthorizedUserModel>
    {
        public UnauthorizedUserModelValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("¡Se requiere usuario!");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("¡Se requiere contraseña!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<UnauthorizedUserModel>.CreateWithOptions((UnauthorizedUserModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
