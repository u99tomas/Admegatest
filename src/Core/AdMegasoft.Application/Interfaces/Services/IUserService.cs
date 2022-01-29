using AdMegasoft.Application.Models;

namespace AdMegasoft.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserModel?> LoginAsync(string name, string password);
        Task<UserModel?> GetUserFromAccessTokenAsync(string accessToken);
    }
}
