using AdMegasoft.Domain.Common;

namespace AdMegasoft.Domain.Entities
{
    public class RolePermissions : IEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
