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

        public Task<User?> GetUserByToken(string token)
        {
            return _userRepository.GetUserByToken(token);
        }

        public Task<User?> Login(User user)
        {
            return _userRepository.Login(user);
        }
    }
}
