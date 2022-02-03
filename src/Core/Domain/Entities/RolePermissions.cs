using Domain.Common;

namespace Domain.Entities
{
    public class RolePermissions : IEntity<int>
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
