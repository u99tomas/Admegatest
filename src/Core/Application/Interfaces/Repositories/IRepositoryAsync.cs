using Domain.Common;

namespace Application.Interfaces.Repositories
{
    public interface IRepositoryAsync<T, in TId> where T : class, IEntity<TId>
    {
        IQueryable<T> Entities { get; }

        IQueryable<T> FromSqlRaw(string sql, params object[] parameters);

        Task<T?> GetByIdAsync(TId id);

        Task<List<T>> ToListAsync();

        Task<T> AddAsync(T entity);

        Task<List<T>> AddRangeAsync(List<T> entities);

        Task RemoveAsync(T entity);
    }
}
