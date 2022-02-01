using AdMegasoft.Application.Features.Users.Queries.GetAllPaged;
using AdMegasoft.Application.Responses;
using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Mappings
{
    public static class UserMappingExtensions
    {
        public static UserResponse ToUserResponse(this User user, IEnumerable<RoleResponse> roles, string accessToken)
        {
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                AccessToken = accessToken,
                Roles = roles
            };
        }

        public static GetAllUsersResponse ToGetAllUsersResponse(this User user)
        {
            return new GetAllUsersResponse
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
            };
        }

        public static IEnumerable<GetAllUsersResponse> ToGetAllUsersResponse(this IEnumerable<User> users)
        {
            return users.Select(u => u.ToGetAllUsersResponse());
        }
    }
}
