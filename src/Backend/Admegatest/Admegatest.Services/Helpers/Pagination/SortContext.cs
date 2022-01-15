using Admegatest.Core.Enums;

namespace Admegatest.Services.Helpers.Pagination
{
    public class SortContext<T>
    {
        public IQueryable<T> Queryable { get; set; }
        public string? SortLabel { get; set; }
        public SortDirection SortDirection { get; set; }

        public SortContext(IQueryable<T> Queryable, AdmTableState admTableState)
        {
            this.Queryable = Queryable;
            SortLabel = admTableState.SortLabel;
            SortDirection = admTableState.SortDirection;
        }
    }
}
