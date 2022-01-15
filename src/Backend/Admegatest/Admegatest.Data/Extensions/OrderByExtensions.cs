using Admegatest.Data.Enums;
using System.Linq.Expressions;

namespace Admegatest.Data.Extensions
{
    public static class OrderByExtensions
    {
        public static IOrderedEnumerable<TSource> OrderByDirection<TSource, TKey>(this IEnumerable<TSource> source, SortDirection direction, Func<TSource, TKey> keySelector)
        {
            if (direction == SortDirection.Descending)
            {
                return source.OrderByDescending(keySelector);
            }

            return source.OrderBy(keySelector);
        }

        public static IOrderedQueryable<TSource> OrderByDirection<TSource, TKey>(this IQueryable<TSource> source, SortDirection direction, Expression<Func<TSource, TKey>> keySelector)
        {
            if (direction == SortDirection.Descending)
            {
                return source.OrderByDescending(keySelector);
            }

            return source.OrderBy(keySelector);
        }
    }
}
