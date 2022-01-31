using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Interfaces.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<List<Role>> GetRolesByUserIdAsync(int userId);
    }
}
