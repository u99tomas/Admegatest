using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Web.Models.Nav;

namespace Web.Shared.Components.Navigation
{
    public partial class NavLinks
    {
        [Parameter]
        public Action<NavLinks> Links { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> _stateTask { get; set; }

        private string _searchString { get; set; }

        private bool _isSearching { get => _searchString != string.Empty; }

        private List<NavGroup> _navGroupsFiltered { get => Filter(_searchString); }

        private List<NavGroup> _navGroups { get; set; }

        private ClaimsPrincipal _user { get; set; }

        public NavLinks()
        {
            _navGroups = new List<NavGroup>();
            _searchString = String.Empty;
        }

        protected async override Task OnInitializedAsync()
        {
            _user = (await _stateTask).User;
            Links(this);
        }

        public NavLinks AddGroup(string groupName)
        {
            _navGroups.Add(new NavGroup(groupName));
            return this;
        }

        public NavLinks AddLink(string linkName, string url, string requiredRole, string icon)
        {
            AddLink(new NavLink(linkName, url, requiredRole, icon));
            return this;
        }

        public NavLinks AddLink(string linkName, string url, string icon)
        {
            AddLink(new NavLink(linkName, url, String.Empty, icon));
            return this;
        }

        private NavLinks AddLink(NavLink navLink)
        {
            var lastGroup = _navGroups.Last();
            var lastElement = lastGroup.NavElements.LastOrDefault();

            if (lastElement != null && lastElement.GetType() == typeof(NavLinkGroup))
            {
                var linkGroup = (NavLinkGroup)lastElement;
                linkGroup.NavLinks.Add(navLink);
                return this;
            }

            lastGroup.NavElements.Add(navLink);

            return this;
        }

        public NavLinks AddLinkGroup(string groupName, string icon)
        {
            _navGroups.Last().NavElements.Add(new NavLinkGroup(groupName, icon));
            return this;
        }

        public List<NavGroup> Filter(string searchString)
        {
            List<NavGroup> navGroupsCopy = _navGroups.Select(ng => ng.Clone()).ToList();

            if (searchString == string.Empty)
            {
                return navGroupsCopy;
            }

            navGroupsCopy = navGroupsCopy.Where(ng =>
            {
                var includeNavGroup = false;

                ng.NavElements = ng.NavElements.Where(ne =>
                {
                    var canViewElement = ne.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase);

                    if (ne.GetType() == typeof(NavLinkGroup) && !canViewElement)
                    {
                        var linkGroup = (NavLinkGroup)ne;
                        linkGroup.NavLinks = linkGroup.NavLinks.Where(nl => nl.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                        canViewElement = linkGroup.NavLinks.Count > 0;
                    }

                    includeNavGroup = canViewElement;

                    return canViewElement;
                }).ToList();

                return includeNavGroup;

            }).ToList();

            return navGroupsCopy;
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
