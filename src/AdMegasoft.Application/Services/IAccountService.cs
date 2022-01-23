namespace AdMegasoft.Application.Services
{
    public interface IAccountService
    {
        // TODO: return UserWithTokenDto, UserWithTokenResponse or LoginAttemptResponse
        void Login(string name, string password); 
    }
}
