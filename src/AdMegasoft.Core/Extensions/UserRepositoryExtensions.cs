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
    }
}
