using AdMegasoft.Web.Extensions;
using AdMegasoft.Web.Models;
using AdMegasoft.Web.Shared.Components.Dialog;
using AdMegasoft.Web.Shared.Components.Table;
using Application.Features.Roles.Commands.Add;
using Application.Features.Roles.Commands.Delete;
using Application.Features.Roles.Queries.GetAllPaged;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Identity.Roles
{
    public partial class Roles
    {
        private MegaTable<GetAllPagedRolesResponse> _table { get; set; }

        private List<GetAllPagedRolesResponse> _roles { get; set; }

        private async Task<TableData<GetAllPagedRolesResponse>> ServerReload(MegaTableState state)
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
            _navigationManager.NavigateTo($"/identity/permissions/{roleId}");
        }

        private async Task EditAsync(int id)
        {
            var parameters = new DialogParameters();
            var role = _roles.FirstOrDefault(r => r.Id == id);

            if (role == null)
            {
                _snackBar.Add("Error de referencia nula", Severity.Error);
                return;
            }

            parameters.Add(nameof(AddEditRoleDialog.Model), new AddEditRoleCommand
            {
                Id = id,
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

        private async Task DeleteAsync(GetAllPagedRolesResponse item)
        {
            var dialog = _dialogService.Show<ConfirmationDialog>();
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var commandResult = await _mediator.Send(new DeleteRoleCommand { Id = item.Id });
                _table.ReloadServerData();
                _snackBar.ShowMessage(commandResult);
            }
        }
    }
}
