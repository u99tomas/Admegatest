using AdMegasoft.Application.Features.Groups.Queries.GetAll;
using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Mappings
{
    public static class GroupMappingExtensions
    {
        public static GetAllGroupsResponse ToGetAllGroupsResponse(this Group group)
        {
            return new GetAllGroupsResponse
            {
                Name = group.Name,
                Description = group.Description,
            };
        }

        public static IEnumerable<GetAllGroupsResponse> ToGetAllGroupsResponse(this IEnumerable<Group> groups)
        {
            return groups.Select(g => g.ToGetAllGroupsResponse());
        }
    }
}
