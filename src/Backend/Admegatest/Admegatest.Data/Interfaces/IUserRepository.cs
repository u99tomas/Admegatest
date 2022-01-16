using Admegatest.Core.Models;

namespace Admegatest.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> LoginAsync(User user);
        public Task<User?> GetUserByTokenAsync(string token);
        public Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
