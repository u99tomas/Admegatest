using Admegatest.Core.Models.Account;

namespace Admegatest.Data.Interfaces.Account
{
    public interface IUserRepository
    {
        public Task<User?> Login(User user);
        public Task<User?> GetUserByToken(string token);
    }
}
