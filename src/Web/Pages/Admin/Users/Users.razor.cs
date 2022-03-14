using Application.Features.Users.Commands.AddEdit;
using Application.Features.Users.Queries.GetAllPaged;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Shared.Constants.Permission;
using Web.Models.Table;
using Web.Shared.Components.Table;

namespace Web.Pages.Admin.Users
{
    public partial class Users
    {
        [CascadingParameter]
        Task<AuthenticationState> StateTask { get; set; }

        private PagedTable<GetAllPagedUsersResponse> _table;

        private List<GetAllPagedUsersResponse> _users { get; set; }

        private bool _canEditUsers { get; set; }

        private bool _canCreateUsers { get; set; }

        protected override async Task<Task> OnInitializedAsync()
        {
            var user = (await StateTask).User;
            _canEditUsers = user.IsInRole(Permissions.Users.Edit);
            _canCreateUsers = user.IsInRole(Permissions.Users.Create);

            return base.OnInitializedAsync();
        }

        private async Task<TableData<GetAllPagedUsersResponse>> ServerReload(PagedTableState state)
        {
            var _result = await _mediator.Send(
                 new GetAllPagedUsersQuery
                 {
                     Page = state.Page,
                     PageSize = state.PageSize,
                     SearchString = state.SearchString,
                     SortDirection = state.SortDirection,
                     SortLabel = state.SortLabel,
                 }
             );

            _users = _result.Data;

            return new TableData<GetAllPagedUsersResponse> { Items = _result.Data, TotalItems = _result.TotalItems };
        }

        private async Task AddAsync()
        {
            var dialog = _dialogService.Show<AddEditUserDialog>();
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                _table.ReloadServerData();
            }
        }

        private async Task EditAsync(int id)
        {
            var parameters = new DialogParameters();
            var user = _users.FirstOrDefault(u => u.Id == id); // Quizas se deba hacer una peticion al mediator por Id y obtener el usuario

            if (user == null)
            {
                _snackBar.Add("Error de referencia nula", Severity.Error);
                return;
            }

            parameters.Add(nameof(AddEditUserDialog.Model), new AddEditUserCommand
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Enabled = user.Enabled,
            });

            var dialog = _dialogService.Show<AddEditUserDialog>("", parameters);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                _table.ReloadServerData();
            }
        }
    }
}
