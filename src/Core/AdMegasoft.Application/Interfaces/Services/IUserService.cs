using AdMegasoft.Application.Models;

namespace AdMegasoft.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserModel?> LoginAsync(UnauthorizedUserModel unauthorizedUserModel);
        Task<UserModel?> GetUserFromAccessTokenAsync(string accessToken);
    }
}
