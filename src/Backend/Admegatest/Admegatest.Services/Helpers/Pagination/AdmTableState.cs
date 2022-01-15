using Admegatest.Core.Enums;

namespace Admegatest.Services.Helpers.Pagination
{
    public class AdmTableState
    {
        public string? SearchString { get; set; }
        public string? SortLabel { get; set; }
        public SortDirection SortDirection { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
