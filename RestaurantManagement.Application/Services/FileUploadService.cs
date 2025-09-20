using Microsoft.AspNetCore.Http;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Common;
using RestSharp;
using System.Net;

namespace RestaurantManagement.Application.Services;

public class FileUploadService : IFileUploadService
{
    private readonly string _filerUrl = "http://192.168.1.2:8888";
    private readonly RestClient _client;

    public FileUploadService()
    {
        _client = new RestClient(_filerUrl);
    }

    public async Task<ServiceResponse<string>> UploadFileAsync(IFormFile file, string category)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return ServiceResponse<string>.BadRequest("File is required");
            }

            // Validate file type (only images)
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return ServiceResponse<string>.BadRequest("Only image files are allowed (jpg, jpeg, png, gif, webp)");
            }

            // Validate file size (max 10MB)
            if (file.Length > 10 * 1024 * 1024)
            {
                return ServiceResponse<string>.BadRequest("File size must be less than 10MB");
            }

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = $"/uploads/{category}/{fileName}";

            var request = new RestRequest(filePath, Method.Post);

            // Convert IFormFile to byte array
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            request.AddFile("file", fileBytes, fileName, file.ContentType);

            var response = await _client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK)
            {
                var fileUrl = $"{_filerUrl}{filePath}";
                return ServiceResponse<string>.Success(fileUrl, "File uploaded successfully");
            }
            else
            {
                return ServiceResponse<string>.Error($"Failed to upload file: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            return ServiceResponse<string>.Error($"Error uploading file: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> DeleteFileAsync(string fileUrl)
    {
        try
        {
            if (string.IsNullOrEmpty(fileUrl))
            {
                return ServiceResponse<object>.BadRequest("File URL is required");
            }

            // Extract file path from URL
            var uri = new Uri(fileUrl);
            var filePath = uri.AbsolutePath;

            var request = new RestRequest(filePath, Method.Delete);
            var response = await _client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.OK)
            {
                return ServiceResponse<object>.Success(null, "File deleted successfully");
            }
            else
            {
                return ServiceResponse<object>.Error($"Failed to delete file: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error deleting file: {ex.Message}");
        }
    }
}
