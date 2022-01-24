using AdMegasoft.Abstractions.Abstractions;
using AdMegasoft.Abstractions.Extensions;
using AdMegasoft.Domain.Entities;
using AdMegasoft.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdMegasoft.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AdMegasoftDbContext context) : base(context, context.Users) { }

        public async Task<User?> GetActiveUserByPasswordNameAsync(string name, string password) =>
            await AsQueryable().WithName(name).WithPassword(password).IsActive().SingleOrDefaultAsync();
    }
}
