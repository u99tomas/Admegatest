﻿using AdMegasoft.Web.Models;
using AdMegasoft.Web.Shared.Components.Table;
using Application.Features.Roles.Queries.GetRolesIdsOfUser;
using Application.Features.Users.Commands.AddEdit;
using Application.Features.Users.Queries.GetAllPaged;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Identity.Users
{
    public partial class Users
    {
        private MegaTable<GetAllPagedUsersResponse> _table;

        private List<GetAllPagedUsersResponse> _users { get; set; }

        private async Task<TableData<GetAllPagedUsersResponse>> ServerReload(MegaTableState state)
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
            var user = _users.FirstOrDefault(u => u.Id == id);

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
                IsActive = user.IsActive,
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
