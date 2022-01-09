namespace Admegatest.Core.Models.Account
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleDescription { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
