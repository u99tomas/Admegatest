using Application.Features.Roles.Commands.Add;
using Application.Features.Roles.Queries.GetAllPaged;

namespace Web.Infrastructure.Mappings
{
    public static class GetAllPagedRolesResponseMappings
    {
        public static AddEditRoleCommand ToAddEditRoleDialog(this GetAllPagedRolesResponse rolesResponse)
        {
            return new AddEditRoleCommand
            {
                Id = rolesResponse.Id,
                Description = rolesResponse.Description,
                Name = rolesResponse.Name,
            };
        }
    }
}
