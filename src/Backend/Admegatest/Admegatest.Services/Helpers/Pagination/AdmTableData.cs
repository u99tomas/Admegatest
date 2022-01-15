namespace Admegatest.Services.Helpers.Pagination
{
    public class AdmTableData<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
