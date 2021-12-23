using Admegatest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Services.IServices
{
    public interface IUserService
    {
        public Task<User> Login(User user);
        public Task<User> Register(User user);
        public Task<User> GetUserByAccessToken(string accessToken);
    }
}
