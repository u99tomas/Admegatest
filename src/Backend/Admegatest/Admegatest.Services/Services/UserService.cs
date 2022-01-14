using Admegatest.Configuration.Account;
using Admegatest.Core.Models.Account;
using Admegatest.Data.DbContexts;
using Admegatest.Data.Repositories.Account;
using Admegatest.Services.Interfaces.Account;
using Microsoft.Extensions.Options;

namespace Admegatest.Services.Services.Account
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(AdmegatestDbContext admegatestDbContext, IOptions<JWTSettings> jwtsettings)
        {
            _userRepository = new UserRepository(admegatestDbContext, jwtsettings);
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return _userRepository.GetAllUsersAsync();
        }

        public Task<User?> GetUserByTokenAsync(string token)
        {
            return _userRepository.GetUserByTokenAsync(token);
        }

        public Task<User?> LoginAsync(User user)
        {
            return _userRepository.LoginAsync(user);
        }
    }
}
