using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<List<Role>> GetRolesByUserIdAsync(int userId);
    }
}
