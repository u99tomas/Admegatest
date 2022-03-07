using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Constants.Permission;

namespace Web.Shared.Components.Navigation
{
    public partial class NavMenu
    {
        [CascadingParameter]
        private Task<AuthenticationState> _stateTask { get; set; }

        private string _searchString { get; set; }

        private bool _canViewUsers { get; set; }
        private bool _canViewRoles { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var user = (await _stateTask).User;

            _canViewUsers = user.IsInRole(Permissions.Users.View);
            _canViewRoles = user.IsInRole(Permissions.Roles.View);
        }
    }
}
