using AdMegasoft.Application.Features.Groups.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Admin.Groups.List
{
    public partial class GroupList
    {
        [Inject]
        private IMediator _mediator { get; set; }
        private IEnumerable<GetAllGroupsResponse> _groups { get; set; }

        private MudTable<GetAllGroupsResponse> table;
        private bool _loading = false;
        private string _searchString = String.Empty;

        private async Task<TableData<GetAllGroupsResponse>> ServerReload(TableState state)
        {
            _loading = true;
            StateHasChanged();

            _groups = await _mediator.Send(new GetAllGroupsQuery());

            #region filters
            if (!string.IsNullOrEmpty(_searchString))
            {
                _groups = _groups.Where(g => g.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase) || 
                g.Description.Contains(_searchString, StringComparison.OrdinalIgnoreCase));
            }

            var totalItems = _groups.Count();

            _groups = _groups.Skip(state.Page * state.PageSize).Take(state.PageSize).ToList();

            switch (state.SortLabel)
            {
                case "Name":
                    _groups = _groups.OrderByDirection(state.SortDirection, g => g.Name);
                    break;

                case "Description":
                    _groups = _groups.OrderByDirection(state.SortDirection, g => g.Description);
                    break;
            }
            #endregion

            _loading = false;
            StateHasChanged();

            return new TableData<GetAllGroupsResponse> { Items = _groups, TotalItems = totalItems };
        }

        private void OnSearch(string text)
        {
            _searchString = text;
            table.ReloadServerData();
        }
    }
}
