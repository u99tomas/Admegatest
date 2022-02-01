﻿using AdMegasoft.Application.Features.Users.Queries.GetAllPaged;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AdMegasoft.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddAdMegasoftApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetAllUsersQuery));
        }
    }
}