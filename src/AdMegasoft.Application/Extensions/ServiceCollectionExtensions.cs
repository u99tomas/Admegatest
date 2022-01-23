using AdMegasoft.Application.Configurations;
using AdMegasoft.Application.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AdMegasoft.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAdMegasoftApplication(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationStateProvider, AdMegasoftAuthenticationStateProvider>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
