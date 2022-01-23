using AdMegasoft.Application.Services.Requests;
using AdMegasoft.Application.Services.Responses;
using AdMegasoft.Core.Entities;

namespace AdMegasoft.Application.Services
{
    public interface IUserService
    {
        Task<LoginAttemptResponse> Login(LoginAttemptRequest loginAttemptRequest);
        Task<UserFromTokenResponse> GetUserFromTokenAsync(UserFromTokenRequest userFromTokenRequest);
    }
}
