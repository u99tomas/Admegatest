using Admegatest.Core.Models;

namespace Admegatest.Data.Interfaces
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetAllRolesOfUserAsync(int userId);
    }
}
