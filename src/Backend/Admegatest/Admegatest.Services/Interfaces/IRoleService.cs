using Admegatest.Core.Models;

namespace Admegatest.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<List<Role>> GetAllRolesOfUserAsync(int userId);
    }
}
