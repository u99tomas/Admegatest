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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database Connection
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("AdMegasoftDb"))
            );
            #endregion

            #region Repositories
            services.AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>));
            #endregion

            #region Services
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            #endregion

            return services;
        }
    }
}
