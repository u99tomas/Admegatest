using Application.Features.Groups.Queries.GetAll;
using Application.Features.Permissions.Queries.ManagePermissions;
using Application.Features.RolePermissions.Commands.Update;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Shared.Constants.Permission;
using Web.Infrastructure.Extensions;
using Web.Models.Table;

namespace Web.Pages.Identity.RolePermissions
{
    public partial class ManagePermissions
    {
        [Parameter]
        public int RoleId { get; set; }

        [CascadingParameter]
        Task<AuthenticationState> StateTask { get; set; }

        private bool _canEditPermissions { get; set; }

        private List<GetAllGroupsResponse> _groups { get; set; } = new();

        private List<ManagePermissionsPermissionsResponse> _permissions { get; set; } = new();

        private MudTabs _tabs { get; set; }

        private int _groupId { get => (int)_tabs.ActivePanel.ID; }

        protected override async Task<Task> OnInitializedAsync()
        {
            _permissions = (await _mediator.Send(new ManagePermissionsPermissionsQuery { RoleId = RoleId })).Data;
            _groups = (await _mediator.Send(new GetAllGroupsQuery())).Data;

            var user = (await StateTask).User;
            _canEditPermissions = user.IsInRole(Permissions.RolePermissions.Edit);

            return base.OnInitializedAsync();
        }

        private async Task<TableData<ManagePermissionsPermissionsResponse>> ServerReload(PagedTableState state)
        {
            if (!_permissions.Any())
            {
                _permissions = (await _mediator.Send(new ManagePermissionsPermissionsQuery { RoleId = RoleId })).Data;
            }

            List<ManagePermissionsPermissionsResponse> data = _permissions.Where(p => p.GroupId == _groupId).ToList();

            if (!string.IsNullOrEmpty(state.SearchString))
            {
                data = data.Where(p => p.Name.Contains(state.SearchString) || p.Description.Contains(state.SearchString))
                    .ToList();
            }

            switch (state.SortLabel)
            {
                case "Name":
                    data = data.OrderBy(r => r.Name, state.SortDirection);
                    break;

                case "Description":
                    data = data.OrderBy(r => r.Description, state.SortDirection);
                    break;

                case "State":
                    data = data.OrderBy(r => r.Assigned, state.SortDirection);
                    break;

                default:
                    data = data.OrderBy(r => r.Name, Application.Enums.SortDirection.Ascending);
                    break;
            }

            var totalItems = data.Count();

            data = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToList();

            return new TableData<ManagePermissionsPermissionsResponse> { Items = data, TotalItems = totalItems };
        }

        public async Task SaveAsync()
        {
            var result = await _mediator.Send(new UpdateRolePermissionsCommand
            {
                RoleId = RoleId,
                PermissionsIds = _permissions.Where(p => p.Assigned).Select(p => p.PermissionId)
            });

            _navigationManager.NavigateTo("/identity/roles");

            _snackBar.ShowMessage(result);
        }

        private Color GetBadgeColor(int selected, int all)
        {
            if (selected == 0)
                return Color.Error;

            if (selected == all)
                return Color.Success;

            return Color.Info;
        }
    }
}
