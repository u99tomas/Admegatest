using Admegatest.Core.Models.Account;

namespace Admegatest.Services.Interfaces.Account
{
    public interface IUserService
    {
        public Task<User?> Login(User user);
        public Task<User?> GetUserByToken(string token);
    }
}
