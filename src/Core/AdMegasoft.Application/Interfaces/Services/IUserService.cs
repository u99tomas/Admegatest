using AdMegasoft.Application.Requests;
using AdMegasoft.Application.Responses;

namespace AdMegasoft.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponse?> LoginAsync(TokenRequest tokenRequest);
        Task<UserResponse?> GetUserFromAccessTokenAsync(string accessToken);
    }
}
