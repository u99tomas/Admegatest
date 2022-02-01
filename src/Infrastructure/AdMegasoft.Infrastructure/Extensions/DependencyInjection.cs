using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Infrastructure.Persistence;
using AdMegasoft.Infrastructure.Repositories;
using AdMegasoft.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdMegasoft.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAdMegasoftInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Persistence
            services.AddDbContext<AdMegasoftDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("AdMegasoftDb"))
            );
            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region Repositories
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            #endregion

            return services;
        }
    }
}
