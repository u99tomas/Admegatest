using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Registra todas las configuraciones de los Entities. <br></br>
        /// Es decir clases que extienden de 
        /// <a href="https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.ientitytypeconfiguration-1?view=efcore-6.0">
        /// IEntityTypeConfiguration</a>
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
