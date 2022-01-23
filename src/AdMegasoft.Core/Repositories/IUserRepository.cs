using AdMegasoft.Core.Models;

namespace AdMegasoft.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByPasswordNameAsync(string name, string password);
    }
}
