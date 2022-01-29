using AdMegasoft.Application.Models;
using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserWithToken> LoginAsync(string name, string password);
        Task<User> GetUserFromAccessTokenAsync(string accessToken);
    }
}
