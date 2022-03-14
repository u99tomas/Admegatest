﻿using System.Data;
using Application.Features.Companies.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class AddEditCompanyCommandValidator : AbstractValidator<AddEditCompanyCommand>
    {
        public AddEditCompanyCommandValidator()
        {
            RuleFor(c => c.CompanyName)
                .NotEmpty()
                .WithMessage("La Razón social es requerida");

            RuleFor(c => c.CompanyName)
                .MaximumLength(100)
                .WithMessage("La Razón social no puede exceder los 100 caracteres");

            RuleFor(c => c.Denomination)
                .MaximumLength(100)
                .WithMessage("La Denominación no puede exceder los 100 caracteres");
        }
    }
}
