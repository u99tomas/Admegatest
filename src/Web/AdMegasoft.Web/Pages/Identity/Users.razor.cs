//using AdMegasoft.Application.Features.Users.Queries.GetAllPaged;
//using MediatR;
//using Microsoft.AspNetCore.Components;
//using MudBlazor;

//namespace AdMegasoft.Web.Pages.Identity
//{
//    public partial class Users
//    {
//        [Inject]
//        private IMediator _mediator { get; set; }
//        private IEnumerable<GetAllUsersResponse> _users { get; set; }

//        private MudTable<GetAllUsersResponse> table;
//        private bool _loading = false;
//        private string _searchString = String.Empty;

//        private async Task<TableData<GetAllUsersResponse>> ServerReload(TableState state)
//        {
//            _loading = true;
//            StateHasChanged();

//            _users = await _mediator.Send(new GetAllUsersQuery());

//            #region filters
//            if (!string.IsNullOrEmpty(_searchString))
//            {
//                _users = _users.Where(u => u.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase));
//            }

//            var totalItems = _users.Count();

//            _users = _users.Skip(state.Page * state.PageSize).Take(state.PageSize).ToList();

//            switch (state.SortLabel)
//            {
//                case "Name":
//                    _users = _users.OrderByDirection(state.SortDirection, u => u.Name);
//                    break;
//            }
//            #endregion

//            _loading = false;
//            StateHasChanged();

//            return new TableData<GetAllUsersResponse> { Items = _users, TotalItems = totalItems };
//        }

//        private void OnSearch(string text)
//        {
//            _searchString = text;
//            table.ReloadServerData();
//        }
//    }
//}
