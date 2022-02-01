using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Domain.Entities;
using AdMegasoft.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AdMegasoft.Infrastructure.Repositories
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(AdMegasoftDbContext context) : base(context, context.Permissions)
        {
        }

        public async Task<List<Permission>> GetPermissionsByUserIdAsync(int userId)
        {
            var sql = @"SELECT P.* FROM UserRoles UR
                        JOIN RolePermissions RP
                            ON UR.RoleId = RP.RoleId
                        JOIN Permissions P
                            ON P.Id = RP.PermissionId
                        WHERE UserId = {0};";

            return await DbSet.FromSqlRaw(sql, userId).ToListAsync();
        }
    }
}
