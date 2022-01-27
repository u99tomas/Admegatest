using AdMegasoft.Domain.Common;

namespace AdMegasoft.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task AddOrUpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<List<T>> ToPaginatedListAsync(int pageNumber, int pageSize);
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> AsQueryable();
    }
}
