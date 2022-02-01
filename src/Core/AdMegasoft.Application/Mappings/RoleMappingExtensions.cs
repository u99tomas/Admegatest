using AdMegasoft.Application.Features.Roles.Queries.GetAll;
using AdMegasoft.Application.Responses;
using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Mappings
{
    public static class RoleMappingExtensions
    {
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
