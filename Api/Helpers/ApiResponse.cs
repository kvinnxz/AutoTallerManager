namespace Api.Helpers;

public record ApiResponse<T>(bool Success, T? Data, string? Message = null)
{
    public static ApiResponse<T> Ok(T data)           => new(true,  data,  null);
    public static ApiResponse<T> Fail(string message) => new(false, default, message);
}

public record ApiResponse(bool Success, string? Message = null)
{
    public static ApiResponse Ok()                    => new(true,  null);
    public static ApiResponse Fail(string message)    => new(false, message);
}
