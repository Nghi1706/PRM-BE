namespace RestaurantManagement.Application.DTOs;

public class CreateStatusCategoryDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid CreatedByUser { get; set; }
}
