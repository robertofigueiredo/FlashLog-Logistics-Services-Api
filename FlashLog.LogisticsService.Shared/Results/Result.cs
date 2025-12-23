namespace FlashLog.LogisticsService.Shared.Results;

public class Result
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public IReadOnlyList<string> Errors { get; init; } = [];

    public static Result Ok(string message = "") => new()
    {
        Success = true,
        Message = message
    };

    public static Result Fail(string message, IEnumerable<string>? errors = null) => new()
    {
        Success = false,
        Message = message,
        Errors = errors?.ToList() ?? []
    };
}

public class Result<T> : Result
{
    public T? Data { get; init; }

    public static Result<T> Ok(T data, string message = "") => new()
    {
        Success = true,
        Data = data,
        Message = message
    };

    public static new Result<T> Fail(string message, IEnumerable<string>? errors = null) => new()
    {
        Success = false,
        Data = default,
        Message = message,
        Errors = errors?.ToList() ?? []
    };
}
