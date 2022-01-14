using Admegatest.Core.Models.Account;

namespace Admegatest.Services.Interfaces.Account
{
    public interface IUserService
    {
        public Task<User?> LoginAsync(User user);
        public Task<User?> GetUserByTokenAsync(string token);
        public Task<List<User>> GetAllUsersAsync();
    }
}
