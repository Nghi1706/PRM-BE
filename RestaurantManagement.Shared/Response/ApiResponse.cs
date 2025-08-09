using System.Net;
namespace RestaurantManagement.Shared.Response;

public class ApiResponse<T>
{
    /// <summary>
    /// The data payload of the response.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// A message providing additional information about the response.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The HTTP status code of the response.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Creates a successful response with the specified data.
    /// </summary>
    public static ApiResponse<T> Success(T data, string message = "Operation completed successfully", HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ApiResponse<T>
        {
            Data = data,
            IsSuccess = true,
            Message = message,
            StatusCode = (int)statusCode
        };
    }

    /// <summary>
    /// Creates a failure response.
    /// </summary>
    public static ApiResponse<T> Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ApiResponse<T>
        {
            Data = default,
            IsSuccess = false,
            Message = message,
            StatusCode = (int)statusCode
        };
    }

    /// <summary>
    /// Creates a created response.
    /// </summary>
    public static ApiResponse<T> Created(T data, string message = "Resource created successfully")
    {
        return new ApiResponse<T>
        {
            Data = data,
            IsSuccess = true,
            Message = message,
            StatusCode = (int)HttpStatusCode.Created
        };
    }

    /// <summary>
    /// Creates an unauthorized response.
    /// </summary>
    public static ApiResponse<T> Unauthorized(string message = "Unauthorized access")
    {
        return new ApiResponse<T>
        {
            Data = default,
            IsSuccess = false,
            Message = message,
            StatusCode = (int)HttpStatusCode.Unauthorized
        };
    }

    /// <summary>
    /// Creates a forbidden response.
    /// </summary>
    public static ApiResponse<T> Forbidden(string message = "Access forbidden")
    {
        return new ApiResponse<T>
        {
            Data = default,
            IsSuccess = false,
            Message = message,
            StatusCode = (int)HttpStatusCode.Forbidden
        };
    }

    /// <summary>
    /// Creates an error response.
    /// </summary>
    public static ApiResponse<T> Error(string message = "Internal server error")
    {
        return new ApiResponse<T>
        {
            Data = default,
            IsSuccess = false,
            Message = message,
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }

    /// <summary>
    /// Creates a not found response.
    /// </summary>
    public static ApiResponse<T> NotFound(string message = "Resource not found")
    {
        return new ApiResponse<T>
        {
            Data = default,
            IsSuccess = false,
            Message = message,
            StatusCode = (int)HttpStatusCode.NotFound
        };
    }
}

// Non-generic version for convenience
public static class ApiResponse
{
    public static ApiResponse<object> Success(object? data = null, string message = "Operation completed successfully", HttpStatusCode statusCode = HttpStatusCode.OK)
        => ApiResponse<object>.Success(data, message, statusCode);

    public static ApiResponse<object> Created(object? data = null, string message = "Resource created successfully")
        => ApiResponse<object>.Created(data, message);

    public static ApiResponse<object> Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => ApiResponse<object>.Failure(message, statusCode);

    public static ApiResponse<object> Unauthorized(string message = "Unauthorized access")
        => ApiResponse<object>.Unauthorized(message);

    public static ApiResponse<object> Forbidden(string message = "Access forbidden")
        => ApiResponse<object>.Forbidden(message);

    public static ApiResponse<object> Error(string message = "Internal server error")
        => ApiResponse<object>.Error(message);

    public static ApiResponse<object> NotFound(string message = "Resource not found")
        => ApiResponse<object>.NotFound(message);
}
