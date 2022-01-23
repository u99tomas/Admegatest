using AdMegasoft.Core.Models;
using AdMegasoft.Core.Repositories;
using AdMegasoft.Infrastructure.Contexts;

namespace AdMegasoft.Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AdMegasoftDbContext context) : base(context, context.Roles) { }

        public Task<List<Role>> GetRolesByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
