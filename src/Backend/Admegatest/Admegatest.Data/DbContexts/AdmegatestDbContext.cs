using Admegatest.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Admegatest.Data.DbContexts
{
    public class AdmegatestDbContext : DbContext
    {
        public AdmegatestDbContext()
        {
        }

        public AdmegatestDbContext(DbContextOptions<AdmegatestDbContext> options) : base(options)
        {
            //// Get the ObjectContext related to this DbContext
            //var objectContext = (this as IObjectContextAdapter).ObjectContext;

            //// Sets the command timeout for all the commands
            //objectContext.CommandTimeout = 300;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
    }
}
