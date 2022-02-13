using Application.Requests;
using Application.Responses;

namespace Application.Interfaces.Services
{
    public interface ICurrentUserService
    {
        Task<UserResponse?> LoginAsync(TokenRequest tokenRequest);
        Task<UserResponse?> GetUserFromAccessTokenAsync(string accessToken);
    }
}
