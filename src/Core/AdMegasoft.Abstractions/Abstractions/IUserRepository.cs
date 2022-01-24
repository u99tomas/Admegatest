using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Abstractions.Abstractions
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetActiveUserByPasswordNameAsync(string name, string password);
    }
}
