namespace AdMegasoft.Application.Models
{
    public class UserWithToken
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string? AccessToken { get; set; }
    }
}
