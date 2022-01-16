using Admegatest.Core.Models;
using Admegatest.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace Admegatest.Web.Pages.Admin.Users
{
    public partial class UserList
    {
        [Inject]
        private IUserService _userService { get; set; }

        private MudTable<User> table;
        private string searchString = null;
        private bool _loading;

        private async Task<TableData<User>> ServerReload(TableState state)
        {
            _loading = true;
            StateHasChanged();

            var users = await _userService.GetAllUsersAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            var totalItems = users.Count();
            users = users.Skip(state.Page * state.PageSize).Take(state.PageSize).ToList();

            switch (state.SortLabel)
            {
                case "Name":
                    users = users.OrderByDirection(state.SortDirection, o => o.Name);
                    break;
            }

            return new TableData<User> { Items = users, TotalItems = totalItems };
        }

        private void RowClickEvent(TableRowClickEventArgs<User> selectedUser)
        {

        }

        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }
    }
}
