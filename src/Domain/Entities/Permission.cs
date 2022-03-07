using Domain.Common;

namespace Domain.Entities
{
    public class Permission : IEntity<int>
    {
        public int Id { get; set; }
        public int PermissionGroupId { get; set; }
        public PermissionGroup PermissionGroup { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
