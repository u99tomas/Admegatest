using Admegatest.Configuration;
using Admegatest.Core.Models;
using Admegatest.Data.DbContexts;
using Admegatest.Data.Extensions;
using Admegatest.Data.Repositories;
using Admegatest.Services.Extensions;
using Admegatest.Services.Helpers.Pagination;
using Admegatest.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Admegatest.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(AdmegatestDbContext admegatestDbContext, IOptions<JWTSettings> jwtsettings)
        {
            _userRepository = new UserRepository(admegatestDbContext, jwtsettings);
        }

        public Task<User?> GetUserByTokenAsync(string token)
        {
            return _userRepository.GetUserByTokenAsync(token);
        }

        public Task<User?> LoginAsync(User user)
        {
            user.Password = user.Password.ToMD5();
            return _userRepository.LoginAsync(user);
        }

        public async Task<AdmTableData<User>> GetUsersAsTableDataAsync(AdmTableState admTableState)
        {
            var queryable = _userRepository.GetUsersAsQueryable();


            if (!string.IsNullOrEmpty(admTableState.SearchString))
            {
                queryable = queryable.Where(u => u.Name.Contains(admTableState.SearchString, StringComparison.OrdinalIgnoreCase));
            }

            switch (admTableState.SortLabel)
            {
                case "Name":
                    queryable = queryable.OrderByDirection(admTableState.SortDirection, o => o.Name);
                    break;
            }

            return await PaginationHelper.GetTableDataAsync<User>(queryable, admTableState);
        }

    }
}
