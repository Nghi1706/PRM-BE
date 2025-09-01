using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Api.Extensions;

public static class ServiceResponseExtensions
{
    public static IResult ToApiResult<T>(this ServiceResponse<T> serviceResponse)
    {
        var apiResponse = serviceResponse.ToApiResponse();
        return Results.Json(apiResponse, statusCode: apiResponse.StatusCode);
    }

    public static IResult ToApiResult(this ServiceResponse<object> serviceResponse)
    {
        var apiResponse = serviceResponse.ToApiResponse();
        return Results.Json(apiResponse, statusCode: apiResponse.StatusCode);
    }
}
