using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetActiveUserByPasswordNameAsync(string name, string password);
    }
}
