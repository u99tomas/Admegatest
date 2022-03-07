using Web.Models.Table;

namespace Web.Mappings
{
    public static class TableStateMappings
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
