using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Web.Managers;
using Web.Models.Nav;

namespace Web.Shared.Components.Navigation
{
    public partial class NavLinks
    {
        [CascadingParameter]
        private Task<AuthenticationState> _stateTask { get; set; }

        [Parameter]
        public NavManager NavManager { get; set; }

        [Parameter]
        public string SearchString { get; set; }

        private List<NavGroup> _navGroups { get => NavManager.Filter(SearchString); }

        private bool _isSearching { get => SearchString != string.Empty; }

        private ClaimsPrincipal _user { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _user = (await _stateTask).User;
        }

        private bool CanViewNavGroup(NavGroup navGroup)
        {
            return navGroup.NavElements.Any(ne =>
            {
                if (ne.GetType() == typeof(NavLinkGroup))
                {
                    var navLinkGroup = (NavLinkGroup)ne;
                    return navLinkGroup.RequiredRoles.Any(r => _user.IsInRole(r));
                }
                else
                {
                    var navLink = (Web.Models.Nav.NavLink)ne;
                    return _user.IsInRole(navLink.RequiredRole) || navLink.RequiredRole == string.Empty;
                }
            });
        }

        private bool CanViewNavLinkGroup(NavLinkGroup navLinkGroup)
        {
            return navLinkGroup.RequiredRoles.Any(r => _user.IsInRole(r) || r == string.Empty);
        }

        private bool CanViewNavLink(NavLink navLink)
        {
            return _user.IsInRole(navLink.RequiredRole) || navLink.RequiredRole == string.Empty;
        }
    }
}
