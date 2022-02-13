
using Application.Wrappers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Mappings
{
    public static class IQueryableMapping
    {
        public static async Task<PagedResult<T>> ToPagedResponseAsync<T>(this IQueryable<T> queryable, int page, int pageSize)
        {
            var totalItems = await queryable.CountAsync();

            var items = await queryable
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return PagedResult<T>.Success(items, totalItems);
        }

        public static IOrderedQueryable<TSource> SortBy<TSource, TKey>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, string sortDirection)
        {
            var isDescending = string.Equals(sortDirection, "Descending", StringComparison.OrdinalIgnoreCase);

            if (isDescending)
                return queryable.OrderByDescending(keySelector);
            else
                return queryable.OrderBy(keySelector);
        }
    }
}
