using Admegatest.Core.Models;
using Admegatest.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Admegatest.Web.Pages.Admin.Roles
{
    public partial class RoleList
    {
        [Inject]
        private IRoleService _roleService { get; set; }

        private MudTable<Role> table;
        private string searchString = null;
        private bool _loading;

        private async Task<TableData<Role>> ServerReload(TableState state)
        {
            _loading = true;
            StateHasChanged();

            var roles = await _roleService.GetAllRolesAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                roles = roles.Where(u =>
                {
                    if(u.RoleName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                        return true;
                    if (u.RoleDescription.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                        return true;
                    return false;
                });
            }

            var totalItems = roles.Count();
            roles = roles.Skip(state.Page * state.PageSize).Take(state.PageSize).ToList();

            switch (state.SortLabel)
            {
                case "RoleName":
                    roles = roles.OrderByDirection(state.SortDirection, o => o.RoleName);
                    break;
                case "RoleDescription":
                    roles = roles.OrderByDirection(state.SortDirection, o => o.RoleDescription);
                    break;
            }

            return new TableData<Role> { Items = roles, TotalItems = totalItems };
        }

        private void RowClickEvent(TableRowClickEventArgs<Role> selectedRole)
        {

        }

        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }
    }
}
