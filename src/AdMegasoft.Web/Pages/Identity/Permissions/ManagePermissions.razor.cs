using AdMegasoft.Web.Models;
using AdMegasoft.Web.Shared.Components.Table;
using Application.Features.Groups.Queries.GetAll;
using Application.Features.Permissions.Queries.ManagePermissions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;

namespace AdMegasoft.Web.Pages.Identity.Permissions
{
    public partial class ManagePermissions
    {
        [Parameter]
        public int RoleId { get; set; }

        private int _tabIndex;

        private int _groupId { get => ++_tabIndex; }

        //private HashSet<int> _selectedPermissions = new HashSet<int>();

        private List<GetAllGroupsResponse> _groups { get; set; } = new();

        private List<GetAllPagedPermissionsResponse> _permissions;

        private MegaTable<GetAllPagedPermissionsResponse> _table;

        protected override async Task<Task> OnInitializedAsync()
        {
            _groups = (await _mediator.Send(new GetAllGroupsQuery())).Data;
            return base.OnInitializedAsync();
        }

        private async Task<TableData<GetAllPagedPermissionsResponse>> ServerReload(MegaTableState state)
        {
            var response = await _mediator.Send(
                 new GetAllPagedPermissionsQuery
                 {
                     RoleId = RoleId,
                     GroupId = _groupId,
                     Page = state.Page,
                     PageSize = state.PageSize,
                     SearchString = state.SearchString,
                     SortDirection = state.SortDirection,
                     SortLabel = state.SortLabel,
                 }
             );

            _permissions = response.Data;
            //_selectedPermissions = new HashSet<int>(_permissions.Select(p => p.Id));
            //StateHasChanged();

            return new TableData<GetAllPagedPermissionsResponse> { Items = _permissions, TotalItems = response.TotalItems };
        }

    }
}
