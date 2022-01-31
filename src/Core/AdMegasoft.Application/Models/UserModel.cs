namespace AdMegasoft.Application.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public IEnumerable<RoleModel> Roles { get; set; }
    }
}
