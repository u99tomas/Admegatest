using AdMegasoft.Web.Models;
using AdMegasoft.Web.Shared.Components.Table;
using Application.Features.Groups.Queries.GetAll;
using Application.Features.Permissions.Queries.GetAllPaged;
using Application.Features.RolePermissions.Commands.Update;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;

namespace AdMegasoft.Web.Pages.Identity.Permissions
{
    public partial class ManagePermissions
    {
        [Parameter]
        public int RoleId { get; set; }

        private List<GetAllGroupsResponse> _groups { get; set; } = new();

        private List<GetAllPagedPermissionsResponse> _permissions { get; set; }

        private List<UpdateRolePermissionsCommand> _updatedPermissions { get; set; } = new();

        private int _tabIndex { get; set; }

        private int _groupId { get => _tabIndex + 1; }

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

            UpdateAssignedState();

            return new TableData<GetAllPagedPermissionsResponse> { Items = _permissions, TotalItems = response.TotalItems };
        }

        private void UpdateAssignedState()
        {
            var ids = (_permissions.Select(p => p.Id));
            var matchingPermissions = _updatedPermissions.Where(up => ids.Contains(up.PermissionId));

            foreach (var permission in matchingPermissions)
            {
                var permissionFound = _permissions.Find(p => p.Id == permission.PermissionId);
                permissionFound.Assigned = permission.Assigned;
            }
        }

        private void AssignedChange(GetAllPagedPermissionsResponse permission, bool assigned)
        {
            permission.Assigned = assigned;

            var foundPermission = _updatedPermissions.Where(up => up.PermissionId == permission.Id)
                .FirstOrDefault();

            if (foundPermission == null)
            {
                _updatedPermissions.Add(new UpdateRolePermissionsCommand
                {
                    PermissionId = permission.Id,
                    Assigned = permission.Assigned
                });
            }
            else
            {
                foundPermission.Assigned = permission.Assigned;
            }
        }
    }
}
