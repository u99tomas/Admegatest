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

        private IEnumerable<User> pagedData;
        private MudTable<User> table;

        private int totalItems;
        private string searchString = null;

        private async Task<TableData<User>> ServerReload(TableState state)
        {
            IEnumerable<User> data = await _userService.GetAllUsersAsync();

            data = data.Where(element =>
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }).ToArray();

            totalItems = data.Count();

            switch (state.SortLabel)
            {
                case "name_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Name);
                    break;
            }

            pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();

            return new TableData<User>() { TotalItems = totalItems, Items = pagedData };
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }
    }
}
