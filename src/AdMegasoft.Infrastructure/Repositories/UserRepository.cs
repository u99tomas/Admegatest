using AdMegasoft.Core.Entities;
using AdMegasoft.Core.Abstractions;
using AdMegasoft.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdMegasoft.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AdMegasoftDbContext context) : base(context, context.Users) { }

        public async Task<User> GetUserByPasswordNameAsync(string name, string password)
        {
            return await DbSet.Where(u => u.Name == name && u.Password == password).SingleOrDefaultAsync();
        }
    }
}
