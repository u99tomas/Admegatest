using Admegatest.Core.Models;

namespace Admegatest.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<IEnumerable<Role>> GetAllRolesOfUserAsync(int userId);
        public Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}
