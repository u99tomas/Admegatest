using AdMegasoft.Web.Infrastructure.Authentication;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AdMegasoft.Web.Infrastructure.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAdMegasoftWebPersistence(this IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            return services;
        }

        public static IServiceCollection AddAdMegasoftWebInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationStateProvider, AdMegasoftAuthenticationStateProvider>();
            return services;
        }
    }
}
