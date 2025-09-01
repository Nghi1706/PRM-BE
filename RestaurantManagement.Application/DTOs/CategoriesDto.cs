namespace RestaurantManagement.Application.DTOs;

public class CategoriesDto
{
    public int M06Id { get; set; }
    public string M06Name { get; set; } = string.Empty;
    public string? M06Description { get; set; }
    public bool M06IsActive { get; set; }
    public Guid M06RestaurantId { get; set; }
    public DateTime? M06CreatedAt { get; set; }
    public Guid? M06CreatedBy { get; set; }
    public DateTime? M06UpdatedAt { get; set; }
    public Guid? M06UpdatedBy { get; set; }
}

public class CreateCategoriesDto
{
    public required string M06Name { get; set; }
    public string? M06Description { get; set; }
    public bool M06IsActive { get; set; } = true;
    public Guid M06RestaurantId { get; set; }
    public Guid? M06CreatedBy { get; set; }
}

public class UpdateCategoriesDto
{
    public string? M06Name { get; set; }
    public string? M06Description { get; set; }
    public bool? M06IsActive { get; set; }
    public Guid? M06UpdatedBy { get; set; }
}