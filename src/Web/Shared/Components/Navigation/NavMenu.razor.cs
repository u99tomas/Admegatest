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

        private string _searchString { get; set; } = String.Empty;

        private NavManager _navManager { get; set; } = new NavManager();

        protected override void OnInitialized()
        {
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
