using AdMegasoft.Web.Models;

namespace AdMegasoft.Web.Mappings
{
    public static class TableState
    {
        public static PagedTableState ToPagedTableState(this MudBlazor.TableState state, string searchString)
        {
            return new PagedTableState
            {
                Page = state.Page,
                PageSize = state.PageSize,
                SearchString = searchString,
                SortDirection = (Application.Enums.SortDirection)state.SortDirection,
                SortLabel = state.SortLabel?? string.Empty,
            };
        }
    }
}
