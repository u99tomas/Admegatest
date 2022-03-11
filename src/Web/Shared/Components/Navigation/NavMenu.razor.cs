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
                .AddLink("Usuarios", "/admin/users", Permissions.Users.View, Icons.Outlined.People)
                .AddLink("Roles", "/admin/roles", Permissions.Roles.View, Icons.Outlined.BackHand)
                .AddLink("Empresas", "/admin/companies", Permissions.Companies.View, Icons.Outlined.Business);
        }

    }
}
