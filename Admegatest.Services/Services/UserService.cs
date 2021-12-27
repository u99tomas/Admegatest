using Admegatest.Core.Models;
using Admegatest.Core.Models.AuthenticationAndAuthorization;
using Admegatest.Data;
using Admegatest.Data.Repositories;
using Admegatest.Services.IServices;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(AdmegatestDBContext admegatestDBContext, IOptions<JWTSettings> jwtsettings)
        {
            _userRepository = new UserRepository(admegatestDBContext, jwtsettings);
        }
        public Task<UserWithToken?> Login(User user)
        {
            return _userRepository.Login(user);
        }

        public Task<User> Register(User user)
        {
            return Task.FromResult(user);
        }

        public Task<User?> GetUserByToken(string token)
        {
            return _userRepository.GetUserByToken(token);
        }
    }
}
