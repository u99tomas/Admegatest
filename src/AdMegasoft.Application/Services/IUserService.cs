using AdMegasoft.Core.Entities;

namespace AdMegasoft.Application.Services
{
    public interface IUserService
    {
        // TODO: return UserWithTokenDto, UserWithTokenResponse or LoginAttemptResponse
        void Login(string name, string password);
        Task<User> GetUserFromTokenAsync(string token);
    }
}
