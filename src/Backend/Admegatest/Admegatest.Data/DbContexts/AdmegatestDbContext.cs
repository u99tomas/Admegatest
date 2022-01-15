using Admegatest.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Admegatest.Data.DbContexts
{
    public class AdmegatestDbContext : DbContext
    {
        public AdmegatestDbContext()
        {

        }

        public AdmegatestDbContext(DbContextOptions<AdmegatestDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
    }
}
