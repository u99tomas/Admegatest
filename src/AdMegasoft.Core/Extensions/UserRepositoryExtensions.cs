using AdMegasoft.Core.Entities;

namespace AdMegasoft.Core.Extensions
{
    public static class UserRepositoryExtensions
    {
        public static IQueryable<User> WithName(this IQueryable<User> queryable, string name)
        {
            return queryable.Where(u => u.Name == name);
        }

        public static IQueryable<User> WithPassword(this IQueryable<User> queryable, string password)
        {
            return queryable.Where(u => u.Password == password);
        }

        public static IQueryable<User> IsActive(this IQueryable<User> queryable)
        {
            return queryable.Where(u => u.IsActive == true);
        }
    }
}
