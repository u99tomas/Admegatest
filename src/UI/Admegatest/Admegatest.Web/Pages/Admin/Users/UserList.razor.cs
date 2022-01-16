using Admegatest.Core.Models;
using Admegatest.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Admegatest.Web.Pages.Admin.Users
{
    public partial class UserList
    {
        [Inject]
        private IUserService _userService { get; set; }

        private MudTable<User> table;
        private string searchString = null;

        private async Task<TableData<User>> ServerReload(TableState state)
        {
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

        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }
    }
}
