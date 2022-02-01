using AdMegasoft.Domain.Common;

namespace AdMegasoft.Domain.Entities
{
    public class UserRoles : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
