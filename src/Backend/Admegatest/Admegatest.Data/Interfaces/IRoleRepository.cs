using Admegatest.Core.Models;

namespace Admegatest.Data.Interfaces
{
    public interface IRoleRepository
    {
        public Task<IEnumerable<Role>> GetAllRolesOfUserAsync(int userId);
        public Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}
