using AdMegasoft.Application.Features.Roles.Queries.GetAll;
using AdMegasoft.Application.Responses;
using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Mappings
{
    public static class RoleMappingExtensions
    {
        public static RoleResponse ToRoleResponse(this Role role)
        {
            return new RoleResponse
            {
                Name = role.Name,
                Description = role.Description,
            };
        }

        public static IEnumerable<RoleResponse> ToRoleResponse(this List<Role> roles)
        {
            return roles.Select(r => new RoleResponse { Name = r.Name, Description = r.Description });
        }

        public static GetAllRolesResponse ToGetAllRolesResponse(this Role role)
        {
            return new GetAllRolesResponse
            {
                Name = role.Name,
                Description = role.Description,
            };
        }

        public static IEnumerable<GetAllRolesResponse> ToGetAllRolesResponse(this IEnumerable<Role> roles)
        {
            return roles.Select(r => ToGetAllRolesResponse(r));
        }
    }
}
