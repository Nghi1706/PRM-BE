using Microsoft.AspNetCore.Http;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Interfaces;

public interface IFileUploadService
{
    Task<ServiceResponse<string>> UploadFileAsync(IFormFile file, string category);
    Task<ServiceResponse<object>> DeleteFileAsync(string fileUrl);
}
