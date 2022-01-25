namespace AdMegasoft.Application.Requests
{
    public class MarkUserAsAuthenticatedRequest
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
