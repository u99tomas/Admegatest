using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Domain.Entities;
using AdMegasoft.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdMegasoft.Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AdMegasoftDbContext context) : base(context, context.Roles) { }

        public async Task<List<Role>> GetRolesByUserIdAsync(int userId)
        {
            var sql = @"SELECT R.* FROM UserGroups UG
                        JOIN GroupRoles GR ON UG.Id = GR.GroupId
                        JOIN Roles R ON R.Id = GR.RoleId
                        WHERE UG.Id = {0}";

            return await DbSet.FromSqlRaw(sql, userId).ToListAsync();
        }
    }
}
