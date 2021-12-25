using Admegatest.Core.Models;
using Admegatest.Core.Models.AuthenticationAndAuthorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Data
{
    public class AdmegatestDbContext : DbContext
    {
        public AdmegatestDbContext(DbContextOptions<AdmegatestDbContext> options) : base(options)
        {

        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
