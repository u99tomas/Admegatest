using AdMegasoft.Web.Services;
using AdMegasoft.Web.Shared.Components.Dialogs;
using Application.Features.Roles.Commands.Add;
using Application.Features.Roles.Commands.Delete;
using Application.Features.Roles.Queries.GetAllPaged;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Identity.Roles
{
    public partial class Roles
    {
        [Inject]
        private ISnackbar _snackbar { get; set; }
        [Inject]
        private IDialogService _dialogService { get; set; }
        [Inject]
        private IMediator _mediator { get; set; }

        private MudTable<GetAllPagedRolesResponse> _table;
        private List<GetAllPagedRolesResponse> _roles;
        private bool _loading = false;
        private string _searchString = String.Empty;

        private async Task<TableData<GetAllPagedRolesResponse>> ServerReload(TableState state)
        {
            ToggleLoading();

            var _response = await _mediator.Send(
                 new GetAllPagedRolesQuery
                 {
                     Page = state.Page,
                     PageSize = state.PageSize,
                     SearchString = _searchString,
                     SortDirection = state.SortDirection.ToString(),
                     SortLabel = state.SortLabel,
                 }
             );

            _roles = _response.Data;

            ToggleLoading();

            return new TableData<GetAllPagedRolesResponse> { Items = _roles, TotalItems = _response.TotalItems };
        }

        private void ToggleLoading()
        {
            _loading = !_loading;
            StateHasChanged();
        }

        private void OnSearch(string text)
        {
            _searchString = text;
            _table.ReloadServerData();
        }

        private async Task ShowDialog(int id = -1)
        {
            var parameters = new DialogParameters();

            if (id != -1)
            {
                var role = _roles.FirstOrDefault(r => r.Id == id);

                parameters.Add(nameof(AddEditRoleDialog.AddEditRoleCommand), new AddEditRoleCommand
                {
                    Id = id,
                    Description = role.Description,
                    Name = role.Name,
                });
            }

            var dialog = _dialogService.Show<AddEditRoleDialog>("", parameters);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await _table.ReloadServerData();
            }
        }

        private async Task Delete(GetAllPagedRolesResponse item)
        {
            var dialog = _dialogService.Show<ConfirmationDialog>();
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var commandResult = await _mediator.Send(new DeleteRoleCommand { Id = item.Id });
                await _table.ReloadServerData();
                _snackbar.ShowMessage(commandResult);
            }
        }
    }
}
