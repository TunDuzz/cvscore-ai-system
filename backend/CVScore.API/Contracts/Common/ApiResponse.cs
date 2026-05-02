namespace CVScore.API.Contracts.Common;

public class ApiResponse<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public ApiErrorResponse? Error { get; init; }

    public static ApiResponse<T> Succeed(T data) => new()
    {
        Success = true,
        Data = data
    };

    public static ApiResponse<T> Fail(string code, string message, IEnumerable<ApiValidationError>? details = null) => new()
    {
        Success = false,
        Error = new ApiErrorResponse
        {
            Code = code,
            Message = message,
            Details = details?.ToArray() ?? []
        }
    };
}

public class ApiResponse
{
    public bool Success { get; init; }
    public ApiErrorResponse? Error { get; init; }

    public static ApiResponse Succeed() => new()
    {
        Success = true
    };

    public static ApiResponse Fail(string code, string message, IEnumerable<ApiValidationError>? details = null) => new()
    {
        Success = false,
        Error = new ApiErrorResponse
        {
            Code = code,
            Message = message,
            Details = details?.ToArray() ?? []
        }
    };
}

public class ApiErrorResponse
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public IReadOnlyCollection<ApiValidationError> Details { get; set; } = [];
    public string? TraceId { get; set; }
}

public class ApiValidationError
{
    public string Field { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
}
