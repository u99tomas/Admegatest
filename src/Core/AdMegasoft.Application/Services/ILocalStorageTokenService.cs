namespace AdMegasoft.Application.Services
{
    public interface ILocalStorageTokenService
    {
        Task<string> GetTokenAsync();
        Task<bool> SaveTokenAsync(string token);
        Task<bool> DestroyTokenAsync();
    }
}
