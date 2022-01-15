﻿using Admegatest.Core.Models;
using Admegatest.Data.DbContexts;
using Admegatest.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Admegatest.Data.Repositories
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
            return await _admegatestDbContext.UserRoles.Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role).ToListAsync();
        }
    }
}
