using Admegatest.Core.Models.Account;

namespace Admegatest.Services.Interfaces.Account
{
    public interface IRoleService
    {
        public Task<List<Role>> GetAllRolesOfUserAsync(int userId);
    }
}
