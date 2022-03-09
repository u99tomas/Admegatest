using Application.Features.Roles.Commands.Add;
using Application.Features.Roles.Queries.GetAllPaged;

namespace Web.Infrastructure.Mappings
{
    public static class GetAllPagedRolesResponseMappings
    {
        public static AddEditRoleCommand ToAddEditRoleDialog(this GetAllPagedRolesResponse rol)
        {
            return new AddEditRoleCommand
            {
                Id = rol.Id,
                Description = rol.Description,
                Name = rol.Name,
            };
        }
    }
}
