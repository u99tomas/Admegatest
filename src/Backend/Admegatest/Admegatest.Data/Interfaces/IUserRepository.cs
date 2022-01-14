using Admegatest.Core.Models.Account;

namespace Admegatest.Data.Interfaces.Account
{
    public interface IUserRepository
    {
        public Task<User?> LoginAsync(User user);
        public Task<User?> GetUserByTokenAsync(string token);
        public Task<List<User>> GetAllUsersAsync();
    }
}
