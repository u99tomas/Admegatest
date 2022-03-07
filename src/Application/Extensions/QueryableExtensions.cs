using System.Linq.Expressions;

namespace Application.Extensions
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, string sortDirection)
        {
            var isDescending = string.Equals(sortDirection, "Descending", StringComparison.OrdinalIgnoreCase);

            if (isDescending)
                return queryable.OrderByDescending(keySelector);
            else
                return queryable.OrderBy(keySelector);
        }
    }
}
