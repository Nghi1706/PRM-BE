using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Api.Extensions;

namespace RestaurantManagement.Api.Endpoints;

public static class FileUploadEndpoints
{
    public static RouteGroupBuilder MapFileUploadEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/upload", async (HttpRequest request, [FromServices] IFileUploadService service) =>
        {
            try
            {
                var form = await request.ReadFormAsync();
                var file = form.Files.GetFile("file");
                var category = form["category"].ToString();

                if (file == null)
                {
                    var errorResponse = ServiceResponse<object>.BadRequest("File is required");
                    return errorResponse.ToApiResult();
                }

                if (string.IsNullOrEmpty(category))
                {
                    var errorResponse = ServiceResponse<object>.BadRequest("Category is required");
                    return errorResponse.ToApiResult();
                }

                var serviceResponse = await service.UploadFileAsync(file, category);

                if (serviceResponse.IsSuccess)
                {
                    var responseDto = new FileUploadResponseDto
                    {
                        FileUrl = serviceResponse.Data ?? "",
                        FileName = file.FileName,
                        FileSize = file.Length,
                        ContentType = file.ContentType
                    };

                    var successResponse = ServiceResponse<FileUploadResponseDto>.Success(responseDto, serviceResponse.Message);
                    return successResponse.ToApiResult();
                }

                return serviceResponse.ToApiResult();
            }
            catch (Exception ex)
            {
                var errorResponse = ServiceResponse<object>.Error($"Error processing upload: {ex.Message}");
                return errorResponse.ToApiResult();
            }
        })
        .DisableAntiforgery()
        .WithName("UploadFile")
        .WithSummary("Upload file to SeaweedFS")
        .WithDescription("Upload image file with category specification")
        .Accepts<IFormFile>("multipart/form-data")
        .Produces<FileUploadResponseDto>(200)
        .Produces(400)
        .Produces(500); // Disable antiforgery for file upload

        group.MapDelete("/delete", async (
            [FromBody] FileDeleteRequestDto dto,
            [FromServices] IFileUploadService service) =>
        {
            var serviceResponse = await service.DeleteFileAsync(dto.FileUrl);
            return serviceResponse.ToApiResult();
        })
        .WithName("DeleteFile")
        .WithSummary("Delete file from SeaweedFS")
        .WithDescription("Delete file by providing the file URL")
        .Accepts<FileDeleteRequestDto>("application/json")
        .Produces(200)
        .Produces(400)
        .Produces(500);

        return group;
    }
}
