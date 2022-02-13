namespace Application.Wrappers
{
    public class Result<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Messages { get; set; }

        public Result(bool succeeded, IEnumerable<string> messages, T data)
        {
            Succeeded = succeeded;
            Messages = messages.ToArray();
            Data = data;
        }

        public Result(bool succeeded, IEnumerable<string> messages)
        {
            Succeeded = succeeded;
            Messages = messages.ToArray();
        }

        public Result(bool succeeded, T data)
        {
            Succeeded = succeeded;
            Data = data;
        }

        public static Result<T> Success(IEnumerable<string> messages, T data)
        {
            return new Result<T>(true, messages, data);
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, data);
        }

        public static Result<T> Failure(IEnumerable<string> messages)
        {
            return new Result<T>(false, messages);
        }
    }
}
