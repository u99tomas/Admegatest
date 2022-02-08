using Application.Features.Users.Queries.GetAllPaged;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Identity.Users
{
    public partial class Users
    {
        [Inject]
        private IDialogService _dialogService { get; set; }
        [Inject]
        private IMediator _mediator { get; set; }

        private MudTable<GetAllPagedUsersResponse> _table;
        private bool _loading = false;
        private string _searchString = String.Empty;

        private async Task<TableData<GetAllPagedUsersResponse>> ServerReload(TableState state)
        {
            ToggleLoading();

            var _response = await _mediator.Send(
                 new GetAllPagedUsersQuery
                 {
                     Page = state.Page,
                     PageSize = state.PageSize,
                     SearchString = _searchString,
                     SortDirection = state.SortDirection.ToString(),
                     SortLabel = state.SortLabel,
                 }
             );

            ToggleLoading();

            return new TableData<GetAllPagedUsersResponse> { Items = _response.Items, TotalItems = _response.TotalItems };
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

        private async Task ShowDialog()
        {
            var dialog = _dialogService.Show<AddEditUserDialog>();
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await _table.ReloadServerData();
            }
        }
    }
}
