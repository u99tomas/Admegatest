using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Domain.Entities;
using AdMegasoft.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AdMegasoft.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AdMegasoftDbContext context) : base(context, context.Users) { }

        public async Task<User?> GetActiveUserByPasswordNameAsync(string name, string password)
        {
            return await AsQueryable().Where(u => u.Name == name && u.Password == password && u.IsActive)
                .FirstOrDefaultAsync();
        }
    }
}
