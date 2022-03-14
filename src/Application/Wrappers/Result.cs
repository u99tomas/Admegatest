namespace Application.Wrappers
{
    public class Result<T>
    {
        public T Data { get; }
        public bool Succeeded { get; }
        public string Message { get; } = string.Empty;
        public bool HasMessage => Message != string.Empty;

        public Result(bool succeeded)
        {
            Succeeded = succeeded;
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

        public Result(bool succeeded, T data)
        {
            Succeeded = succeeded;
            Data = data;
        }

        public static Result<T> Success(string message, T data)
        {
            return new Result<T>(true, message, data);
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, data);
        }

        public static Result<T> Failure(string message)
        {
            return new Result<T>(false, message);
        }
    }
}
