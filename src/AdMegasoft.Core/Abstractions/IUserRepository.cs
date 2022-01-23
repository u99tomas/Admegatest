using AdMegasoft.Core.Entities;

namespace AdMegasoft.Core.Abstractions
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByPasswordNameAsync(string name, string password);
    }
}
