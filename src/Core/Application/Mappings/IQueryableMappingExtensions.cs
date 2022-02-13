﻿
using Application.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Application.Mappings
{
    public static class IQueryableMappingExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResponseAsync<T>(this IQueryable<T> queryable, int page, int pageSize)
        {
            var totalItems = await queryable.CountAsync();

            var items = await queryable
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalItems = totalItems
            };
        }
    }
}
