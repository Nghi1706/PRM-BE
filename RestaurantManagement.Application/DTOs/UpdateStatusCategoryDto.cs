namespace RestaurantManagement.Application.DTOs;

public class UpdateStatusCategoryDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
    public Guid? UpdatedByUser { get; set; }
}
