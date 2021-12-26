using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Core.Models.AuthenticationAndAuthorization
{
    public class UserWithToken : User
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(User user)
        {
            this.UserId = user.UserId;
            this.Role = user.Role;
            this.Name = user.Name;
            this.Password = user.Password;
        }
    }
}
