using Admegatest.Core.Models;
using Admegatest.Core.Models.AuthenticationAndAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Data.InterfacesForRepositories
{
    public interface IUserRepository
    {
        public Task<UserWithToken> Login(User user);
        public Task<User> GetUserByAccessToken(string accessToken);
    }
}
