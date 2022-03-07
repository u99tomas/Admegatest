using AdMegasoft.Web.Extensions;
using AdMegasoft.Web.Models;
using AdMegasoft.Web.Shared.Components.Dialog;
using AdMegasoft.Web.Shared.Components.Table;
using Application.Features.Roles.Commands.Add;
using Application.Features.Roles.Commands.Delete;
using Application.Features.Roles.Queries.GetAllPaged;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Shared.Constants.Permission;

namespace AdMegasoft.Web.Pages.Identity.Roles
{
    public partial class Roles
    {
        [CascadingParameter]
        Task<AuthenticationState> StateTask { get; set; }

        private PagedTable<GetAllPagedRolesResponse> _table { get; set; }

        private List<GetAllPagedRolesResponse> _roles { get; set; }

        private bool _canEditRoles { get; set; }

        private bool _canCreateRoles { get; set; }

        private bool _canDeleteRoles { get; set; }

        private bool _canViewRolePermissions { get; set; }

        protected override async Task<Task> OnInitializedAsync()
        {
            var user = (await StateTask).User;
            _canEditRoles = user.IsInRole(Permissions.Roles.Edit);
            _canCreateRoles = user.IsInRole(Permissions.Roles.Create);
            _canDeleteRoles = user.IsInRole(Permissions.Roles.Delete);
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

            parameters.Add(nameof(AddEditRoleDialog.Model), new AddEditRoleCommand
            {
                Id = role.Id,
                Description = role.Description,
                Name = role.Name,
            });

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
