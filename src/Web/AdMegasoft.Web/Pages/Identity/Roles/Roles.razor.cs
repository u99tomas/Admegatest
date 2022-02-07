using Application.Features.Roles.Commands.Delete;
using Application.Features.Roles.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Client.Shared.Dialogs;
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

            ToggleLoading();

            return new TableData<GetAllPagedRolesResponse> { Items = _response.Items, TotalItems = _response.TotalItems };
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

        private async Task ShowAddRoleDialogAsync()
        {
            var dialog = _dialogService.Show<AddRoleDialog>();
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await _table.ReloadServerData();
            }
        }

        private async Task Delete(GetAllPagedRolesResponse item)
        {
            var parameters = new DialogParameters
            {
                {nameof(DeleteConfirmationDialog.ContentText), $"¿Deseas eliminar el rol {item.Name}?"}
            };

            var dialog = _dialogService.Show<DeleteConfirmationDialog>("", parameters);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var response = await _mediator.Send(new DeleteRoleCommand { Id = item.Id });
                await _table.ReloadServerData();
                _snackbar.Add($"Se ha eliminado el Rol: {item.Name}", Severity.Success);
            }
        }
    }
}
