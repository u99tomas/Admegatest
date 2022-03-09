using Application.Interfaces.Base;

namespace Web.Models.Nav
{
    public class NavLink : INavElement, ICloneable<NavLink>
    {
        public string Name { get; }
        public string Url { get; }
        public string RequiredRole { get; }
        public string Icon { get; }

        public NavLink(string name, string url, string requiredRole, string icon)
        {
            Name = name;
            Url = url;
            RequiredRole = requiredRole;
            Icon = icon;
        }

        public NavLink Clone() => new NavLink(Name, Url, RequiredRole, Icon);

    }
}
