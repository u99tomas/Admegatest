namespace AdMegasoft.Application.Services
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync();
        Task<bool> SaveTokenAsync(string token);
        Task<bool> DestroyTokenAsync();
        int GetUserIdFromToken(string token);
    }
}
