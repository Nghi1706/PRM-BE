namespace RestaurantManagement.Application.DTOs;

public class StatusCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
public class CreateStatusCategoryDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid CreatedByUser { get; set; }
}
public class UpdateStatusCategoryDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
    public Guid? UpdatedByUser { get; set; }
}


