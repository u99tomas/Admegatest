using Admegatest.Core.Models;
using Admegatest.Services.Helpers.Pagination;

namespace Admegatest.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User?> LoginAsync(User user);
        public Task<User?> GetUserByTokenAsync(string token);
        public Task<AdmTableData<User>> GetUsersAsTableDataAsync(AdmTableState admTableState);
    }
}
