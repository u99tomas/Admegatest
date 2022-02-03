using Application.Interfaces.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAdMegasoftInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Persistence
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("AdMegasoftDb"))
            );
            #endregion

            #region Services
            #endregion

            #region Repositories
            services.AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>));
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            #endregion

            return services;
        }
    }
}
