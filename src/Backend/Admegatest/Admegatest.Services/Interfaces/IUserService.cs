using Admegatest.Core.Models;

namespace Admegatest.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User?> LoginAsync(User user);
        public Task<User?> GetUserByTokenAsync(string token);
        public Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
