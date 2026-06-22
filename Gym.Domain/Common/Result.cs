namespace Gym.Domain.Common;

    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; }
        public string ErrorCode { get; }

        protected Result(bool isSuccess, string error = "", string errorCode = "")
        {
            IsSuccess = isSuccess;
            Error = error;
            ErrorCode = errorCode;
        }

        public static Result Ok() => new(true);
        public static Result Ok(string message) => new(true);
        public static Result Fail(string error, string errorCode = "") => new(false, error, errorCode);

        public static Result<T> Ok<T>(T value) => new(value, true);
        public static Result<T> Fail<T>(string error, string errorCode = "") => new(default!, false, error, errorCode);
    }

public class Result<T> : Result
{
    public T Value { get; }

    internal Result(T value, bool isSuccess, string error = "", string errorCode = "")
        : base(isSuccess, error, errorCode)
    {
        Value = value;
    }
}
