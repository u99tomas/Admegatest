namespace Application.Wrappers
{
    public class PagedResult<T> : Result<T>
    {
        public new List<T> Data { get; set; }
        public int TotalItems { get; set; }

        public PagedResult(bool succeeded, List<T> data, int totalItems)
        {
            Succeeded = succeeded;
            Data = data;
            TotalItems = totalItems;
        }

        public static PagedResult<T> Success(List<T> data, int totalItems)
        {
            return new PagedResult<T>(true, data, totalItems);
        }
    }
}
