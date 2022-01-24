using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Abstractions.Abstractions
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<List<Role>> GetRolesByUserIdAsync(int userId);
    }
}
