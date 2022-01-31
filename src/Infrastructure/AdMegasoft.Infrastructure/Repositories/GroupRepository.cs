using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Domain.Entities;
using AdMegasoft.Infrastructure.Persistence;

namespace AdMegasoft.Infrastructure.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(AdMegasoftDbContext context) : base(context, context.Groups)
        {
        }
    }
}
