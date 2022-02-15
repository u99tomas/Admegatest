using Application.Features.Users.Commands.AddEdit;
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
        private List<GetAllPagedUsersResponse> _users;
        private bool _loading = false;
        private string _searchString = String.Empty;

        private async Task<TableData<GetAllPagedUsersResponse>> ServerReload(TableState state)
        {
            ToggleLoading();

            var _result = await _mediator.Send(
                 new GetAllPagedUsersQuery
                 {
                     Page = state.Page,
                     PageSize = state.PageSize,
                     SearchString = _searchString,
                     SortDirection = state.SortDirection.ToString(),
                     SortLabel = state.SortLabel,
                 }
             );

            _users = _result.Data;

            ToggleLoading();

            return new TableData<GetAllPagedUsersResponse> { Items = _result.Data, TotalItems = _result.TotalItems };
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
                var user = _users.FirstOrDefault(u => u.Id == id);

                parameters.Add(nameof(AddEditUserDialog.AddEditUserCommand), new AddEditUserCommand
                {
                    Id = user.Id,
                    AccountName = user.AccountName,
                    Password = user.Password,
                });
            }

            var dialog = _dialogService.Show<AddEditUserDialog>("", parameters);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await _table.ReloadServerData();
            }
        }
    }
}
