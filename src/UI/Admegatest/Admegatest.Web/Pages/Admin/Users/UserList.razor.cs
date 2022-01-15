using Admegatest.Core.Models;
using Admegatest.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
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
            var data = _userService.GetUsersAsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(u => u.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            switch (state.SortLabel)
            {
                case "Name":
                    data = data.OrderByDirection(state.SortDirection, o => o.Name);
                    break;
            }

            totalItems = await data.CountAsync();

            var users = await data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToListAsync();

            return new TableData<User>() { TotalItems = totalItems, Items = users };
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }
    }
}
