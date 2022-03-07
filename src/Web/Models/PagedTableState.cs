using Application.Enums;

namespace Web.Models
{
    public class PagedTableState
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public SortDirection SortDirection { get; set; }
        public string SortLabel { get; set; }
    }
}
