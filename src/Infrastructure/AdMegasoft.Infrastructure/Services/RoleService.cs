using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<Role>> GetRolesByUserIdAsync(int userId)
        {
            return await _roleRepository.GetRolesByUserIdAsync(userId);
        }
    }
}
