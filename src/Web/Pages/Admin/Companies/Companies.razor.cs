using Application.Features.Companies.Queries.GetAllPaged;
using Application.Features.Companies.Queries.GetById;
using Application.Features.Roles.Queries.GetAllPaged;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Shared.Constants.Permission;
using Web.Infrastructure.Extensions;
using Web.Models.Table;
using Web.Pages.Admin.Roles;
using Web.Shared.Components.Table;

namespace Web.Pages.Admin.Companies
{
    public partial class Companies
    {
        [CascadingParameter]
        Task<AuthenticationState> StateTask { get; set; }

        private PagedTable<GetAllPagedCompaniesResponse> _table { get; set; }

        private List<GetAllPagedCompaniesResponse> _companies { get; set; }

        private bool _canEditCompany { get; set; }

        private bool _canCreateCompany { get; set; }

        protected override async Task<Task> OnInitializedAsync()
        {
            var user = (await StateTask).User;

            _canEditCompany = user.IsInRole(Permissions.Companies.Edit);
            _canCreateCompany = user.IsInRole(Permissions.Companies.Create);

            return base.OnInitializedAsync();
        }

        private async Task<TableData<GetAllPagedCompaniesResponse>> ServerReload(PagedTableState state)
        {
            var response = await _mediator.Send(
                 new GetAllPagedCompaniesQuery
                 {
                     Page = state.Page,
                     PageSize = state.PageSize,
                     SearchString = state.SearchString,
                     SortDirection = state.SortDirection,
                     SortLabel = state.SortLabel,
                 }
             );

            _companies = response.Data;

            return new TableData<GetAllPagedCompaniesResponse> { Items = _companies, TotalItems = response.TotalItems };
        }

        //private async Task AddAsync()
        //{
        //    var dialog = _dialogService.Show<AddEditRoleDialog>();
        //    var result = await dialog.Result;

        //    if (!result.Cancelled)
        //    {
        //        _table.ReloadServerData();
        //    }
        //}

        private async Task EditAsync(int companyId)
        {
            var result = await _mediator.Send(new GetByIdCompanyQuery {Id = companyId});
            _snackBar.ShowMessage(result);
            //var parameters = new DialogParameters();

            //parameters.Add(nameof(AddEditRoleDialog.Model), role.ToAddEditRoleDialog());

            //var dialog = _dialogService.Show<AddEditRoleDialog>("", parameters);
            //var result = await dialog.Result;

            //if (!result.Cancelled)
            //{
            //    _table.ReloadServerData();
            //}
        }
    }
}
