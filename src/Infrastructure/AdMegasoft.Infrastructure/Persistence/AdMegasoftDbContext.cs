using AdMegasoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AdMegasoft.Infrastructure.Persistence
{
    public class AdMegasoftDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserGroups> UserGroups { get; set; }
        public DbSet<GroupRoles> GroupRoles { get; set; }

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
