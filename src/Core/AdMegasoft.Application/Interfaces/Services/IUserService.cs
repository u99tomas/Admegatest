using AdMegasoft.Application.Models;
using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserModel?> LoginAsync(string name, string password);
        Task<UserModel?> GetUserFromAccessTokenAsync(string accessToken);
    }
}
