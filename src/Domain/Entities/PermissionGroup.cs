using Domain.Common;

namespace Domain.Entities
{
    public class PermissionGroup : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
