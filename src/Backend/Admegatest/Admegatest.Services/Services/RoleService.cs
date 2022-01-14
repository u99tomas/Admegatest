using Admegatest.Core.Models.Account;
using Admegatest.Data.DbContexts;
using Admegatest.Data.Repositories.Account;
using Admegatest.Services.Interfaces.Account;

namespace Admegatest.Services.Services.Account
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
