using AdMegasoft.Application.Requests;
using AdMegasoft.Application.Responses;

namespace AdMegasoft.Application.Services
{
    public interface IUserService
    {
        Task<LoginAttemptResponse> Login(LoginAttemptRequest loginAttemptRequest);
        Task<UserFromTokenResponse> GetUserFromTokenAsync(string token);
    }
}
