using System.ComponentModel.DataAnnotations.Schema;

namespace Admegatest.Core.Models.Account
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
