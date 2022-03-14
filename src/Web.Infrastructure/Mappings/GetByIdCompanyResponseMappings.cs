using Application.Features.Companies.Commands;
using Application.Features.Companies.Queries.GetAllPaged;
using Application.Features.Companies.Queries.GetById;

namespace Web.Infrastructure.Mappings
{
    public static class GetByIdCompanyResponseMappings
    {
        public static AddEditCompanyCommand ToAddEditCompanyCommand(this GetByIdCompanyResponse response)
        {
            return new AddEditCompanyCommand
            {
                Id = response.Id,
                CompanyName = response.CompanyName,
                Denomination = response.Denomination
            };
        }
    }
}
