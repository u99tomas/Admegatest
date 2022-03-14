using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class MegaDbContext : DbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<UserRoles> UserRoles { get; }
        public DbSet<Role> Roles { get; }
        public DbSet<Permission> Permissions { get; }
        public DbSet<RolePermissions> RolePermissions { get; }
        public DbSet<PermissionGroup> PermissionGroups { get; }

        public MegaDbContext(DbContextOptions<MegaDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
