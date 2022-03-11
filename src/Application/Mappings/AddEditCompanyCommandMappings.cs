using Application.Features.Companies.Commands;
using Domain.Entities;

namespace Application.Mappings
{
    public static class AddEditCompanyCommandMappings
    {
        public static Company ToCompany(this AddEditCompanyCommand command)
        {
            return new Company
            {
                Id = command.Id,
                CompanyName = command.CompanyName,
                Denomination = command.Denomination,
            };
        }
    }
}
