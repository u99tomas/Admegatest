using AdMegasoft.Application.Features.Roles.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Admin.Roles.List
{
    public partial class RoleList
    {
        [Inject]
        private IMediator _mediator { get; set; }
        private IEnumerable<GetAllRolesResponse> _roles { get; set; }

        private MudTable<GetAllRolesResponse> table;
        private bool _loading = false;
        private string _searchString = String.Empty;

        private async Task<TableData<GetAllRolesResponse>> ServerReload(TableState state)
        {
            _loading = true;
            StateHasChanged();

            _roles = await _mediator.Send(new GetAllRolesQuery());

            #region filters
            if (!string.IsNullOrEmpty(_searchString))
            {
                _roles = _roles.Where(u => u.Name.Contains(
                    _searchString, StringComparison.OrdinalIgnoreCase) ||
                    u.Description.Contains(_searchString, StringComparison.OrdinalIgnoreCase)
                    );
            }

            var totalItems = _roles.Count();

            _roles = _roles.Skip(state.Page * state.PageSize).Take(state.PageSize).ToList();

            switch (state.SortLabel)
            {
                case "Name":
                    _roles = _roles.OrderByDirection(state.SortDirection, o => o.Name);
                    break;

                case "Description":
                    _roles = _roles.OrderByDirection(state.SortDirection, o => o.Description);
                    break;
            }
            #endregion

            _loading = false;
            StateHasChanged();

            return new TableData<GetAllRolesResponse> { Items = _roles, TotalItems = totalItems };
        }

        private void OnSearch(string text)
        {
            _searchString = text;
            table.ReloadServerData();
        }
    }
}
