namespace RestaurantManagement.Application.DTOs;

public class UpdateRestaurantDto
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedByUser { get; set; }
}
