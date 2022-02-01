using AdMegasoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AdMegasoft.Infrastructure.Persistence
{
    public class AdMegasoftDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }

        public AdMegasoftDbContext(DbContextOptions<AdMegasoftDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
