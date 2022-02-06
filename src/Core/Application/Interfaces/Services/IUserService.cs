using Application.Requests;
using Application.Responses;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponse?> LoginAsync(TokenRequest tokenRequest);
        Task<UserResponse?> GetUserFromAccessTokenAsync(string accessToken);
    }
}
