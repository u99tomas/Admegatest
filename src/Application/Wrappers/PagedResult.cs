namespace Application.Wrappers
{
    public class PagedResult<T> : Result<T>
    {
        public new bool Succeeded { get; }
        public new List<T> Data { get; }
        public int TotalItems { get; }

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
