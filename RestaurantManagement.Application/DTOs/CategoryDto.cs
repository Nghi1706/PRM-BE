namespace RestaurantManagement.Application.DTOs;

public class CategoryDto
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedByUser { get; set; }
}

public class CreateCategoryDto
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; } = default!;
    public bool? IsActive { get; set; }
    public string? Description { get; set; }
    public Guid? CreatedByUser { get; set; }
}

public class UpdateCategoryDto
{
    public Guid? RestaurantId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
    public Guid? UpdatedByUser { get; set; }
}