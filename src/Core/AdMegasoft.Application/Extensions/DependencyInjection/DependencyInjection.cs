using AdMegasoft.Application.Features.Users.Queries.GetAllPaged;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AdMegasoft.Application.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddAdMegasoftApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetAllUsersQuery)); // TODO: add with reflection
        }
    }
}
