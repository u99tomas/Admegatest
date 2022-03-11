using Application.Features.Roles.Commands.Delete;
using Application.Features.Roles.Queries.GetAllPaged;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Shared.Constants.Permission;
using Web.Infrastructure.Extensions;
using Web.Infrastructure.Mappings;
using Web.Models.Table;
using Web.Shared.Components.Dialog;
using Web.Shared.Components.Table;

namespace Web.Pages.Admin.Roles
{
    public partial class Roles
    {
        [CascadingParameter]
        Task<AuthenticationState> StateTask { get; set; }

        private PagedTable<GetAllPagedRolesResponse> _table { get; set; }

        private List<GetAllPagedRolesResponse> _roles { get; set; }

        private bool _canEditRole { get; set; }

        private bool _canCreateRole { get; set; }

        private bool _canDeleteRole { get; set; }

        private bool _canViewRolePermissions { get; set; }

        protected override async Task<Task> OnInitializedAsync()
        {
            var user = (await StateTask).User;

            _canEditRole = user.IsInRole(Permissions.Roles.Edit);
            _canCreateRole = user.IsInRole(Permissions.Roles.Create);
            _canDeleteRole = user.IsInRole(Permissions.Roles.Delete);
            _canViewRolePermissions = user.IsInRole(Permissions.RolePermissions.View);

            return base.OnInitializedAsync();
        }

        private async Task<TableData<GetAllPagedRolesResponse>> ServerReload(PagedTableState state)
        {
            var response = await _mediator.Send(
                 new GetAllPagedRolesQuery
                 {
                     Page = state.Page,
                     PageSize = state.PageSize,
                     SearchString = state.SearchString,
                     SortDirection = state.SortDirection,
                     SortLabel = state.SortLabel,
                 }
             );

            _roles = response.Data;

            return new TableData<GetAllPagedRolesResponse> { Items = _roles, TotalItems = response.TotalItems };
        }

        private async Task AddAsync()
        {
            var dialog = _dialogService.Show<AddEditRoleDialog>();
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                _table.ReloadServerData();
            }
        }

        private void ManagePermissions(int roleId)
        {
            _navigationManager.NavigateTo($"/identity/role/permissions/{roleId}");
        }

        private async Task EditAsync(GetAllPagedRolesResponse role)
        {
            var parameters = new DialogParameters();

            parameters.Add(nameof(AddEditRoleDialog.Model), role.ToAddEditRoleCommand());

            var dialog = _dialogService.Show<AddEditRoleDialog>("", parameters);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                _table.ReloadServerData();
            }
        }

        private async Task DeleteAsync(int id)
        {
            var dialog = _dialogService.Show<ConfirmationDialog>();
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var commandResult = await _mediator.Send(new DeleteRoleCommand { Id = id });
                _table.ReloadServerData();
                _snackBar.ShowMessage(commandResult);
            }
        }
    }
}
