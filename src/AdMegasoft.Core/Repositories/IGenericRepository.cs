using AdMegasoft.Core.Common;

namespace AdMegasoft.Core.Repositories
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task AddOrUpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
