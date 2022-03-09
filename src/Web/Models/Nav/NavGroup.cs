using Application.Interfaces.Base;

namespace Web.Models.Nav
{
    public class NavGroup : INavElement, ICloneable<NavGroup>
    {
        public string Name { get; }
        public List<INavElement> NavElements { get; set; }

        public NavGroup(string name)
        {
            Name = name;
            NavElements = new List<INavElement>();
        }

        public NavGroup(string name, List<INavElement> navElements)
        {
            Name = name;
            NavElements = navElements;
        }

        public NavGroup Clone()
        {
            var elements = NavElements.Select(ne =>
            {
                if (ne.GetType() == typeof(NavLinkGroup))
                {
                    return (INavElement)((NavLinkGroup)ne).Clone();
                }
                else
                {
                    return (INavElement)((NavLink)ne).Clone();
                }
            }).ToList();

            return new NavGroup(Name, elements);
        }
    }
}
