using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Interfaces.Repositories
{
    public interface IPermissionRepository : IGenericRepository<Permission>
    {
        Task<List<Permission>> GetPermissionsByUserIdAsync(int userId);
    }
}
