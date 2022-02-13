using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddPersistence(services, configuration);
            AddServices(services);
            AddRepositories(services);

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MegaDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("AdMegasoftDb"))
            );

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ICurrentUserService, CurrentUserService>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
            .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
            .AddTransient<IRoleRepository, RoleRepository>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IPermissionRepository, PermissionRepository>()
            .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }
    }
}
