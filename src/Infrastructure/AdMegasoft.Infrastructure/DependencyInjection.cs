using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Infrastructure.Persistence;
using AdMegasoft.Infrastructure.Repositories;
using AdMegasoft.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdMegasoft.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAdMegasoftPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdMegasoftDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("AdMegasoftDb"))
            );

            return services;
        }

        public static IServiceCollection AddAdMegasoftInfrastructure(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IUserService, UserService>();

            // Repositories
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();

            return services;
        }
    }
}
