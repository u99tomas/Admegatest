using AdMegasoft.Domain.Common;

namespace AdMegasoft.Abstractions.Abstractions
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task AddOrUpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> AsQueryable();
    }
}
