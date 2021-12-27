using Admegatest.Core.Models.AuthenticationAndAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
