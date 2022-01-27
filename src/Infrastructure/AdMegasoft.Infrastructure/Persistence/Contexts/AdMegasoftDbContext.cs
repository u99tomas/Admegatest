using AdMegasoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdMegasoft.Infrastructure.Persistence.Contexts
{
    public class AdMegasoftDbContext : DbContext
    {
        public AdMegasoftDbContext(DbContextOptions<AdMegasoftDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserGroups> UserGroups { get; set; }
        public DbSet<GroupRoles> GroupRoles { get; set; }
    }
}
