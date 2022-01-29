using AdMegasoft.Application.Models;
using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Mappings
{
    public static class UserMappingExtensions
    {
        public static UserModel ToModel(this User user, IEnumerable<RoleModel> roles, string accessToken)
        {
            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                AccessToken = accessToken,
                Roles = roles
            };
        }
    }
}
