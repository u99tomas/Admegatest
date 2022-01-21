using Admegatest.Core.Models;
using Admegatest.Data.DbContexts;
using Admegatest.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Admegatest.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AdmegatestDbContext _context;

        public RoleRepository(AdmegatestDbContext admegatestDBContext)
        {
            _context = admegatestDBContext;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetAllRolesOfUserAsync(int userId)
        {
            return await _context.UserRoles.Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role).ToListAsync();
        }
    }
}
