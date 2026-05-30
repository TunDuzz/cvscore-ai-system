namespace CVScore.Application.Common;

public class ServiceResult
{
    public bool IsSuccess { get; init; }
    public string? ErrorCode { get; init; }
    public string? ErrorMessage { get; init; }

    public static ServiceResult Success() => new()
    {
        IsSuccess = true
    };

    public static ServiceResult Failure(string errorCode, string errorMessage) => new()
    {
        IsSuccess = false,
        ErrorCode = errorCode,
        ErrorMessage = errorMessage
    };
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; init; }

    public static ServiceResult<T> Success(T data) => new()
    {
        IsSuccess = true,
        Data = data
    };

    public static new ServiceResult<T> Failure(string errorCode, string errorMessage) => new()
    {
        IsSuccess = false,
        ErrorCode = errorCode,
        ErrorMessage = errorMessage
    };
}
