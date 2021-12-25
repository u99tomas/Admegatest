using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Core.Models.AuthenticationAndAuthorization
{
    public class RefreshToken
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public virtual User User { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
