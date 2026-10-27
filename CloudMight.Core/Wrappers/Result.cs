namespace CloudMight.Core.Wrappers;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public static Result Success() => new Result() { IsSuccess = true };
    public static Result Failure(string error) => new Result() { IsSuccess = false, ErrorMessage = error };
    public static Result Failure(IEnumerable<string> errors) => new Result() { IsSuccess = false, Errors = errors };
}

public class Result<T> : Result
{
    public T Data { get; set; }
    public static Result<T> Success(T data) => new Result<T>() { IsSuccess = true , Data = data};
    public new static Result<T> Failure(string error) => new Result<T>() { IsSuccess = false, ErrorMessage = error };
}