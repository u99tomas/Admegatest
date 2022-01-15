using Admegatest.Core.Models;
using Admegatest.Services.Helpers.Pagination;
using Admegatest.Services.Interfaces;
using Admegatest.Web.Mappings;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Admegatest.Web.Pages.Admin.Users
{
    public partial class UserList
    {
        [Inject]
        private IUserService _userService { get; set; }

        private IEnumerable<User> pagedData;
        private MudTable<User> table;

        private int totalItems;
        private string searchString = null;

        private async Task<TableData<User>> ServerReload(TableState state)
        {
            var admTableData = await _userService.GetUsersAsTableDataAsync(state.ToAdmTableState(searchString));
            return admTableData.ToTableData();
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }
    }
}
