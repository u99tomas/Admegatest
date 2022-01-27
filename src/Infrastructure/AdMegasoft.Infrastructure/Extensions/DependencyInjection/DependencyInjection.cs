using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Infrastructure.Persistence.Contexts;
using AdMegasoft.Infrastructure.Repositories;
using AdMegasoft.Infrastructure.Services;
using AdMegasoft.Shared.Constants.Application;
using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdMegasoft.Infrastructure.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAdMegasoftPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBlazoredLocalStorage();

            services.AddDbContext<AdMegasoftDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString(ApplicationSettingsConstants.Database))
            );

            return services;
        }

        public static IServiceCollection AddAdMegasoftInfrastructure(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJWTService, JWTService>();

            // Repositories
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
