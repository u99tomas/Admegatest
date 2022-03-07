using Application.Interfaces.Base;

namespace Web.Models.Nav
{
    public class NavLinkGroup : INavElement, ICloneable<NavLinkGroup>
    {
        public string Name { get; }
        public string Icon { get; set; }
        public List<NavLink> NavLinks { get; set; }
        public IEnumerable<string> RequiredRoles { get => NavLinks.Select(n => n.RequiredRole); }

        public NavLinkGroup(string name, string icon)
        {
            Name = name;
            Icon = icon;
            NavLinks = new List<NavLink>();
        }

        public NavLinkGroup(string name, string icon, List<NavLink> navLinks)
        {
            Name = name;
            Icon = icon;
            NavLinks = navLinks;
        }

        public NavLinkGroup Clone() => new NavLinkGroup(Name, Icon, NavLinks.Select(n => n.Clone()).ToList());

    }
}
