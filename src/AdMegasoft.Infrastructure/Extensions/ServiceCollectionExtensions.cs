using AdMegasoft.Core.Abstractions;
using AdMegasoft.Infrastructure.Contexts;
using AdMegasoft.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdMegasoft.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdMegasoftInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdMegasoftDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("AdMegasoftDb"))
                );

            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
