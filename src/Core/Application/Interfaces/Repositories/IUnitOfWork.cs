using Domain.Common;

namespace Application.Interfaces.Repositories
{
    public interface IUnitOfWork<TId> : IDisposable
    {
        IRepositoryAsync<T, TId> Repository<T>() where T : class, IEntity<TId>;

        Task<int> CommitAsync(CancellationToken cancellationToken);

        Task Rollback();
    }
}
