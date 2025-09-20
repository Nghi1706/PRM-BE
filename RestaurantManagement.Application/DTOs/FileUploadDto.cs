namespace RestaurantManagement.Application.DTOs;

public class FileUploadRequestDto
{
    public required string Category { get; set; } // "user", "restaurant", "dish", etc.
}

public class FileUploadResponseDto
{
    public string FileUrl { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string ContentType { get; set; } = string.Empty;
}

public class FileDeleteRequestDto
{
    public required string FileUrl { get; set; }
}
