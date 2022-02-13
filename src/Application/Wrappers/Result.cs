namespace Application.Wrappers
{
    public class Result<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public Result()
        {

        }

        public Result(bool succeeded, string message, T data)
        {
            Succeeded = succeeded;
            Message = message;
            Data = data;
        }

        public Result(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public static Result<T> Success(string message, T data)
        {
            return new Result<T>(true, message, data);
        }

        public static Result<T> Failure(string message)
        {
            return new Result<T>(false, message);
        }
    }
}
