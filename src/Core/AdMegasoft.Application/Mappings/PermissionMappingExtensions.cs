using AdMegasoft.Application.Responses;
using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Mappings
{
    public static class PermissionMappingExtensions
    {
        public static PermissionResponse ToPermissionResponse(this Permission permission)
        {
            return new PermissionResponse
            {
                Name = permission.Name,
                Description = permission.Description,
            };
        }

        public static IEnumerable<PermissionResponse> ToPermissionResponse(this IEnumerable<Permission> permissions)
        {
            return permissions.Select(p => ToPermissionResponse(p));
        }
    }
}
