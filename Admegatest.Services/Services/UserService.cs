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
        public Task<User> GetUserByAccessToken(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> Login(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> RegisterUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
