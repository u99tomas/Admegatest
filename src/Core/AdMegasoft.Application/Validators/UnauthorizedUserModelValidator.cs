﻿using AdMegasoft.Application.Models;
using FluentValidation;

namespace AdMegasoft.Application.Validators
{
    public class UnauthorizedUserModelValidator : GenericValidator<UnauthorizedUserModel>
    {
        public UnauthorizedUserModelValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("¡Se requiere usuario!");

            RuleFor(u => u.Name)
                .MinimumLength(5)
                .WithMessage("¡El usuario Ingresado es invalido!");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("¡Se requiere contraseña!");

            RuleFor(u => u.Password)
                .MinimumLength(5)
                .WithMessage("¡El contraseña Ingresada es invalida!");
        }
    }
}
