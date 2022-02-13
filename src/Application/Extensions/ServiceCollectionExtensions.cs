﻿using Application.Features.Roles.Queries.GetAllPaged;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetAllPagedRolesQuery));
        }
    }
}
