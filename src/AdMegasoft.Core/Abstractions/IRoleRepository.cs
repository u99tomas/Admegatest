using AdMegasoft.Core.Entities;

namespace AdMegasoft.Core.Abstractions
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<List<Role>> GetRolesByUserIdAsync(int userId);
    }
}
