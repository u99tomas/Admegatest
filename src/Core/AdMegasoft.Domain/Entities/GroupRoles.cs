using AdMegasoft.Domain.Common;

namespace AdMegasoft.Domain.Entities
{
    public class GroupRoles : IEntity
    {
        public int Id { get; set; }
        public Group Group { get; set; }
        public int GroupId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
