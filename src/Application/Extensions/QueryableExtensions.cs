using Application.Enums;
using System.Linq.Expressions;

namespace Application.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, SortDirection sortDirection)
        {
            if (sortDirection == SortDirection.Descending)
            {
                return queryable.OrderByDescending(keySelector);
            }
            else if (sortDirection == SortDirection.Ascending)
            {
                return queryable.OrderBy(keySelector);
            }

            return queryable;
        }

        public static IQueryable<TSource> Filter<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, bool>> predicate, string searchString)
        {
            if (searchString == string.Empty) return queryable;
            return queryable.Where(predicate);
        }
    }
}
