namespace Admegatest.Services.Helpers.Pagination
{
    public class SearchContext<T>
    {
        public IQueryable<T> Queryable { get; set; }
        public string? SearchString { get; set; }
        public SearchContext(IQueryable<T> Queryable, AdmTableState admTableState)
        {
            this.Queryable = Queryable;
            SearchString = admTableState.SearchString;
        }
    }
}
