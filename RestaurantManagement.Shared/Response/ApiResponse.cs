
public class ApiResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public object? Data { get; set; }

    public static ApiResponse Success(object? data = null, string message = "Success", int statusCode = 200)
        => new ApiResponse { StatusCode = statusCode, Message = message, Data = data };

    public static ApiResponse Fail(string message, int statusCode = 400)
        => new ApiResponse { StatusCode = statusCode, Message = message, Data = null };
}
