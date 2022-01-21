using Admegatest.Configuration;
using Admegatest.Core.Models;
using Admegatest.Data.DbContexts;
using Admegatest.Data.Repositories;
using Admegatest.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Admegatest.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(AdmegatestDbContext admegatestDbContext, IOptions<JWTSettings> jwtsettings)
        {
            _userRepository = new UserRepository(admegatestDbContext, jwtsettings);
        }

        public Task<User?> GetUserByTokenAsync(string token)
        {
            return _userRepository.GetUserByTokenAsync(token);
        }

        public Task<User?> LoginAsync(User user)
        {
            //user.Password = user.Password.ToMD5();
            return _userRepository.LoginAsync(user);
        }
        
        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return _userRepository.GetAllUsersAsync();
        }
    }
}
