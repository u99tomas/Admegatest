namespace AdMegasoft.Core.Models
{
    public class UserGroups
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Group Group { get; set; }
        public int GroupId { get; set; }
    }
}
