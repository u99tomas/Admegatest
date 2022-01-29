using AdMegasoft.Application.Models;

namespace AdMegasoft.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserWithToken> LoginAsync(string name, string password);
    }
}
