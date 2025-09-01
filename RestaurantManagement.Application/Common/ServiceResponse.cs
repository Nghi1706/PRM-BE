using System.Net;
using RestaurantManagement.Shared.Response;

namespace RestaurantManagement.Application.Common;

public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public List<string> Errors { get; set; } = new();

    public static ServiceResponse<T> Success(T data, string message = "Operation completed successfully", HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResponse<T>
        {
            Data = data,
            IsSuccess = true,
            Message = message,
            StatusCode = (int)statusCode,
            Errors = new List<string>()
        };
    }

    public static ServiceResponse<T> Created(T data, string message = "Resource created successfully")
    {
        return new ServiceResponse<T>
        {
            Data = data,
            IsSuccess = true,
            Message = message,
            StatusCode = (int)HttpStatusCode.Created,
            Errors = new List<string>()
        };
    }

    public static ServiceResponse<T> NotFound(string message = "Resource not found")
    {
        return new ServiceResponse<T>
        {
            Data = default,
            IsSuccess = false,
            Message = message,
            StatusCode = (int)HttpStatusCode.NotFound,
            Errors = new List<string>()
        };
    }

    public static ServiceResponse<T> BadRequest(string message = "Bad request", List<string>? errors = null)
    {
        return new ServiceResponse<T>
        {
            Data = default,
            IsSuccess = false,
            Message = message,
            StatusCode = (int)HttpStatusCode.BadRequest,
            Errors = errors ?? new List<string>()
        };
    }

    public static ServiceResponse<T> Error(string message = "Internal server error", List<string>? errors = null)
    {
        return new ServiceResponse<T>
        {
            Data = default,
            IsSuccess = false,
            Message = message,
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Errors = errors ?? new List<string>()
        };
    }

    public static ServiceResponse<T> Unauthorized(string message = "Unauthorized access")
    {
        return new ServiceResponse<T>
        {
            Data = default,
            IsSuccess = false,
            Message = message,
            StatusCode = (int)HttpStatusCode.Unauthorized,
            Errors = new List<string>()
        };
    }

    public static ServiceResponse<T> Forbidden(string message = "Access forbidden")
    {
        return new ServiceResponse<T>
        {
            Data = default,
            IsSuccess = false,
            Message = message,
            StatusCode = (int)HttpStatusCode.Forbidden,
            Errors = new List<string>()
        };
    }

    // Convert to ApiResponse
    public ApiResponse<T> ToApiResponse()
    {
        if (IsSuccess)
        {
            return StatusCode == 201
                ? ApiResponse<T>.Created(Data, Message)
                : ApiResponse<T>.Success(Data, Message, (HttpStatusCode)StatusCode);
        }
        else
        {
            return StatusCode switch
            {
                404 => ApiResponse<T>.NotFound(Message),
                400 => ApiResponse<T>.Failure(Message, HttpStatusCode.BadRequest),
                401 => ApiResponse<T>.Unauthorized(Message),
                403 => ApiResponse<T>.Forbidden(Message),
                _ => ApiResponse<T>.Error(Message)
            };
        }
    }
}

// Non-generic version
public static class ServiceResponse
{
    public static ServiceResponse<object> Success(object? data = null, string message = "Operation completed successfully", HttpStatusCode statusCode = HttpStatusCode.OK)
        => ServiceResponse<object>.Success(data, message, statusCode);

    public static ServiceResponse<object> Created(object? data = null, string message = "Resource created successfully")
        => ServiceResponse<object>.Created(data, message);

    public static ServiceResponse<object> NotFound(string message = "Resource not found")
        => ServiceResponse<object>.NotFound(message);

    public static ServiceResponse<object> BadRequest(string message = "Bad request", List<string>? errors = null)
        => ServiceResponse<object>.BadRequest(message, errors);

    public static ServiceResponse<object> Error(string message = "Internal server error", List<string>? errors = null)
        => ServiceResponse<object>.Error(message, errors);

    public static ServiceResponse<object> Unauthorized(string message = "Unauthorized access")
        => ServiceResponse<object>.Unauthorized(message);

    public static ServiceResponse<object> Forbidden(string message = "Access forbidden")
        => ServiceResponse<object>.Forbidden(message);
}
