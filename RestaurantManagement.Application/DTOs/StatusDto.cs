namespace RestaurantManagement.Application.DTOs;

public class StatusDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = default!;
    public int Code { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
