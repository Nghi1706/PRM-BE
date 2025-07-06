namespace RestaurantManagement.Application.DTOs;

public class CreateStatusDto
{
    public required Guid CategoryId { get; set; }
    public string Name { get; set; } = default!;
    public required int Code { get; set; }
    public string? Description { get; set; }
    public Guid? CreatedByUser { get; set; }
    public bool? IsActive { get; set; }
}
