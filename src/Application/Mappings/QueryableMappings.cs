
using Application.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Application.Mappings
{
    public static class QueryableMappings
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> queryable, int page, int pageSize)
        {
            var totalItems = await queryable.CountAsync();

            var items = await queryable
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return PagedResult<T>.Success(items, totalItems);
        }
    }
}
