using Admegatest.Core.Models;
using Admegatest.Core.Models.AuthenticationAndAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Services.IServices
{
    public interface IUserService
    {
        public Task<UserWithToken?> Login(User user);
        public Task<User> Register(User user);
        public Task<User?> GetUserByAccessToken(string token);
    }
}
