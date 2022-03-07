using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Shared.Constants.Permission;
using System.Security.Claims;
using Web.Managers;
using Web.Models.Nav;

namespace Web.Shared.Components.Navigation
{
    public partial class NavMenu
    {
        [CascadingParameter]
        private Task<AuthenticationState> _stateTask { get; set; }

        private string _searchString { get; set; } = String.Empty;

        private bool _isSearching { get => _searchString != string.Empty; }

        private NavManager _navManager { get; set; } = new NavManager();

        private List<NavGroup> _navGroups { get => _navManager.Filter(_searchString); }

        private ClaimsPrincipal _user { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _user = (await _stateTask).User;

            _navManager
                .AddGroup("Dashboard")
                .AddLink("Dashboard", "/content/dashboard", Icons.Material.Outlined.Dashboard)
                .AddGroup("Páginas")
                .AddLinkGroup("Administrador", Icons.Outlined.AdminPanelSettings)
                .AddLink("Usuarios", "/identity/users", Permissions.Users.View, Icons.Outlined.People)
                .AddLink("Roles", "/identity/roles", Permissions.Roles.View, Icons.Outlined.BackHand);
        }
    }
}
