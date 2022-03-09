using MudBlazor;
using Shared.Constants.Permission;

namespace Web.Shared.Components.Navigation
{
    public partial class NavMenu
    {
        private void Links(NavLinks links)
        {
            links
                .AddGroup("Dashboard")
                .AddLink("Dashboard", "/content/dashboard", Icons.Material.Outlined.Dashboard)
                .AddGroup("Páginas")
                .AddLinkGroup("Administrador", Icons.Outlined.AdminPanelSettings)
                .AddLink("Usuarios", "/identity/users", Permissions.Users.View, Icons.Outlined.People)
                .AddLink("Roles", "/identity/roles", Permissions.Roles.View, Icons.Outlined.BackHand);
        }

    }
}
