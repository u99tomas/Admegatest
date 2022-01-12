using Admegatest.Core.Models.Account;

namespace Admegatest.Services.Interfaces.Account
{
    public interface IRoleService
    {
        public Task<List<Role>> GetAllRolesOfUser(int userId);
    }
}
