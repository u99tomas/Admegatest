using Admegatest.Core.Models;
using Admegatest.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Services.Services
{
    public class UserService : IUserService
    {
        public Task<User> Login(User user)
        {
            user.AccessToken = "AccessToken123";
            user.RefreshToken = "RefreshToken123";
            user.Role = new Role { RoleDescription = "IsCustomer", RoleId = 1};
            return Task.FromResult(user);
        }

        public Task<User> Register(User user)
        {
            return Task.FromResult(user);
        }

        public Task<User> GetUserByAccessToken(string accessToken)
        {
            return Task.FromResult(new User { Email = "admegatest@gmail.com", Password = "Megasoft", Role = new Role { RoleDescription = "IsCustomer", RoleId = 1 } });
        }
    }
}
