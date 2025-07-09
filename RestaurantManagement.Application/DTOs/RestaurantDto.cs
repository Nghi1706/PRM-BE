namespace RestaurantManagement.Application.DTOs;

public class RestaurantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedByUser { get; set; }
}