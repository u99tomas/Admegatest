using AdMegasoft.Application.Requests;
using AdMegasoft.Application.Responses;

namespace AdMegasoft.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> LoginAsync(LoginAttemptRequest loginAttemptRequest);
        Task LogoutAsync();
        Task<UserFromTokenResponse?> GetUserFromTokenAsync(string token);
    }
}
