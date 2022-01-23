using AdMegasoft.Core.Common;

namespace AdMegasoft.Core.Entities
{
    public class UserGroups : Entity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Group Group { get; set; }
        public int GroupId { get; set; }
    }
}
