namespace AdMegasoft.Core.Models
{
    public class GroupRoles
    {
        public int Id { get; set; }
        public Group Group { get; set; }
        public int GroupId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
