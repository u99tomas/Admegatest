using Admegatest.Core.Models.Account;
using Admegatest.Data.DbContexts;
using Admegatest.Data.Interfaces.Account;
using Microsoft.EntityFrameworkCore;

namespace Admegatest.Data.Repositories.Account
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AdmegatestDbContext _admegatestDbContext;

        public RoleRepository(AdmegatestDbContext admegatestDBContext)
        {
            _admegatestDbContext = admegatestDBContext;
        }
        public async Task<List<Role>> GetAllRolesOfUserAsync(int userId)
        {
            //return await _admegatestDbContext.UserRoles.Where(ur => ur.UserId == userId)
            //    .Select(ur => ur.Role).ToListAsync();

            var sql = @"SELECT * FROM dbo.Roles r
                        WHERE r.Id IN 
                        (SELECT RoleId FROM UserRoles WHERE UserId = 1)";

            var roles = await _admegatestDbContext.Roles.FromSqlRaw(sql).ToListAsync();

            return roles;
        }
    }
}
