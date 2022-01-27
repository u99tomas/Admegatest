using AdMegasoft.Application.Extensions.Repositories;
using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Domain.Entities;
using AdMegasoft.Infrastructure.Persistence.Contexts;
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
