using Application.Enums;

namespace AdMegasoft.Web.Extensions
{
    public static class ListExtensions
    {
        public static List<TSource> OrderBy<TSource, TKey>(this List<TSource> list, Func<TSource, TKey> keySelector, SortDirection sortDirection)
        {
            if (sortDirection == SortDirection.Descending)
            {
                return list.OrderByDescending(keySelector).ToList();
            }
            else if (sortDirection == SortDirection.Ascending)
            {
                return list.OrderBy(keySelector).ToList();
            }

            return list;
        }
    }
}
