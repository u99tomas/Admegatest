namespace AdMegasoft.Application.Responses
{
    public class UserFromTokenResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool FoundAUser { get; set; }
    }
}
