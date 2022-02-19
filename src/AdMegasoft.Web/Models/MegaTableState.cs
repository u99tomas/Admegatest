namespace AdMegasoft.Web.Models
{
    public class MegaTableState
    {
        public int Page { get; }
        public int PageSize { get; }
        public string SearchString { get; }
        public string SortDirection { get; }
        public string SortLabel { get; }

        public MegaTableState(int page, int pageSize, string sortDirection, string sortLabel, string searchString)
        {
            Page = page;
            PageSize = pageSize;
            SortDirection = sortDirection;
            SortLabel = sortLabel;
            SearchString = searchString;
        }
    }
}
