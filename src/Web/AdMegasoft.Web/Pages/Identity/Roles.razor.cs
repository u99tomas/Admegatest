using Application.Features.Roles.Queries.GetAllPaged;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Identity
{
    public partial class Roles
    {
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
    }
}
