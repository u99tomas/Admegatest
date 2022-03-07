using Web.Models.Nav;

namespace Web.Managers
{
    public interface INavManager
    {
        INavManager AddGroup(string groupName);
        INavManager AddLinkGroup(string groupName, string icon);
        INavManager AddLink(string linkName, string url, string role, string icon);
        INavManager AddLink(string linkName, string url, string role);
        List<NavGroup> Filter(string searchString);
    }
}
