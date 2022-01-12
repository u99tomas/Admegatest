using Admegatest.Core.Models.Account;

namespace Admegatest.Data.Interfaces.Account
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetAllRolesOfUser(int userId);
    }
}
