using Application.Features.Roles.Commands.Add;
using Domain.Entities;

namespace Application.Mappings
{
    public static class AddEditRoleCommandMappings
    {
        public static Role ToRole(this AddEditRoleCommand roleCommand)
        {
            return new Role
            {
                Id = roleCommand.Id,
                Name = roleCommand.Name,
                Description = roleCommand.Description,
            };
        }
    }
}
