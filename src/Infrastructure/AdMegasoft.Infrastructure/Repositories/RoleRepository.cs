using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Domain.Entities;
using AdMegasoft.Infrastructure.Persistence.Contexts;

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
