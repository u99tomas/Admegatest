using Admegatest.Core.Models;
using Admegatest.Data.DbContexts;
using Admegatest.Data.Repositories;
using Admegatest.Services.Interfaces;

namespace Admegatest.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService(AdmegatestDbContext admegatestDbContext)
        {
            _roleRepository = new RoleRepository(admegatestDbContext);
        }

        public Task<List<Role>> GetAllRolesOfUserAsync(int userId)
        {
            return _roleRepository.GetAllRolesOfUserAsync(userId);
        }
    }
}
