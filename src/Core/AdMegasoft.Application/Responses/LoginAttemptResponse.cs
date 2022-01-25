namespace AdMegasoft.Application.Responses
{
    public class LoginAttemptResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
    }
}
