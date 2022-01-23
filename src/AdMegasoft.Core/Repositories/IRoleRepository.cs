using AdMegasoft.Core.Models;

namespace AdMegasoft.Core.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<List<Role>> GetRolesByUserIdAsync(int userId);
    }
}
