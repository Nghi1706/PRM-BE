namespace RestaurantManagement.Application.DTOs;

public class CreateRestaurantDto
{
    public required string Name { get; set; }
    public string? Address { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public bool? IsActive { get; set; }
    public Guid? CreatedByUser { get; set; }
}