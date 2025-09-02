namespace RestaurantManagement.Application.DTOs;

public class DishesDto
{
    public int M07Id { get; set; }
    public string M07Name { get; set; } = string.Empty;
    public string? M07Description { get; set; }
    public decimal M07Price { get; set; }
    public string M07Image { get; set; } = string.Empty;
    public bool M07IsActive { get; set; }
    public int M07CategoryId { get; set; }
    public Guid M07RestaurantId { get; set; }
    public DateTime? M07CreatedAt { get; set; }
    public Guid? M07CreatedBy { get; set; }
    public DateTime? M07UpdatedAt { get; set; }
    public Guid? M07UpdatedBy { get; set; }
}

public class CreateDishesDto
{
    public required string M07Name { get; set; }
    public string? M07Description { get; set; }
    public decimal M07Price { get; set; }
    public required string M07Image { get; set; }
    public bool M07IsActive { get; set; } = true;
    public int M07CategoryId { get; set; }
    public Guid M07RestaurantId { get; set; }
    public Guid? M07CreatedBy { get; set; }
}

public class UpdateDishesDto
{
    public string? M07Name { get; set; }
    public string? M07Description { get; set; }
    public decimal? M07Price { get; set; }
    public string? M07Image { get; set; }
    public bool? M07IsActive { get; set; }
    public int? M07CategoryId { get; set; }
    public Guid? M07RestaurantId { get; set; }
    public Guid? M07UpdatedBy { get; set; }
}