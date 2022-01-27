using AdMegasoft.Application.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AdMegasoft.Application.Extensions.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAdMegasoftApplication(this IServiceCollection services)
        {
            // TODO: or addSingleton?
            services.AddScoped<AuthenticationStateProvider, AdMegasoftAuthenticationStateProvider>();
        }
    }
}
