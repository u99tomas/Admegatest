using AdMegasoft.Application.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AdMegasoft.Application.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddAdMegasoftApplication(this IServiceCollection services)
        {
            // TODO: or addSingleton?
            services.AddScoped<AuthenticationStateProvider, AdMegasoftAuthenticationStateProvider>();
            // TODO: configurar JWTSettings
        }
    }
}
