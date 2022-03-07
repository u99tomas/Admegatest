using Web.Models.Nav;

namespace Web.Managers
{
    public class NavManager : INavManager
    {
        private List<NavGroup> _navGroups { get; set; }

        public NavManager()
        {
            _navGroups = new List<NavGroup>();
        }

        public INavManager AddGroup(string groupName)
        {
            _navGroups.Add(new NavGroup(groupName));
            return this;
        }

        public INavManager AddLink(string linkName, string url, string requiredRole, string icon)
        {
            AddLink(new NavLink(linkName, url, requiredRole, icon));
            return this;
        }

        public INavManager AddLink(string linkName, string url, string icon)
        {
            AddLink(new NavLink(linkName, url, String.Empty, icon));
            return this;
        }

        private INavManager AddLink(NavLink navLink)
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

        public INavManager AddLinkGroup(string groupName, string icon)
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

    }
}
