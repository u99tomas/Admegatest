namespace AdMegasoft.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T model);
        Task UpdateAsync(T model);
        Task AddOrUpdateAsync(T model);
        Task DeleteAsync(T model);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
