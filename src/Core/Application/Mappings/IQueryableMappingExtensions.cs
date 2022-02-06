
using Application.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Mappings
{
    public static class IQueryableMappingExtensions
    {
        public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(this IQueryable<T> queryable, int page, int pageSize)
        {
            var items = await queryable
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalItems = await queryable.CountAsync();

            return new PagedResponse<T>
            {
                Items = items,
                TotalItems = totalItems
            };
        }
    }
}
