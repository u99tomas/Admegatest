using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Infrastructure.Persistence.Contexts;
using AdMegasoft.Infrastructure.Repositories;
using AdMegasoft.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdMegasoft.Infrastructure.Extensions.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdMegasoftInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<AdMegasoftDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("AdMegasoftDb")) // TODO: AdMegasoftDb should by a const?
                );

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
