using AdMegasoft.Application.Features.Roles.Commands.AddEdit;
using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Mappings
{
    public static class AddEditRoleCommandMappingExtensions
    {
        public static Role ToRole(this AddEditRoleCommand addEditRoleCommand)
        {
            return new Role
            {
                Id = addEditRoleCommand.Id,
                Name = addEditRoleCommand.Name,
                Description = addEditRoleCommand.Description,
            };
        }
    }
}
