using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Extensions.Repositories
{
    public static class UserRepositoryExtensions
    {
        public static IQueryable<User> WithName(this IQueryable<User> q, string name)
        {
            return q.Where(u => u.Name == name);
        }

        public static IQueryable<User> WithPassword(this IQueryable<User> q, string password)
        {
            return q.Where(u => u.Password == password);
        }

        public static IQueryable<User> IsActive(this IQueryable<User> q)
        {
            return q.Where(u => u.IsActive == true);
        }
    }
}
