using AdMegasoft.Application.Features.Roles.Queries.GetAll;
using AdMegasoft.Application.Models;
using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Mappings
{
    public static class RoleMappingExtensions
    {
        public static RoleModel ToModel(this Role role)
        {
            return new RoleModel
            {
                Name = role.Name,
                Description = role.Description,
            };
        }

        public static IEnumerable<RoleModel> ToModel(this List<Role> roles)
        {
            return roles.Select(r => new RoleModel { Name = r.Name, Description = r.Description });
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
