namespace RestaurantManagement.Application.DTOs;

public class RestaurantsDto
{
    public Guid M04Id { get; set; }
    public string M04Name { get; set; } = string.Empty;
    public string? M04Address { get; set; }
    public string M04Phone { get; set; } = string.Empty;
    public string M04Email { get; set; } = string.Empty;
    public string? M04Logo { get; set; }
    public bool M04IsActive { get; set; }
    public DateTime? M04CreatedAt { get; set; }
    public Guid? M04CreatedBy { get; set; }
    public DateTime? M04UpdatedAt { get; set; }
    public Guid? M04UpdatedBy { get; set; }
}

public class CreateRestaurantsDto
{
    public required string M04Name { get; set; }
    public string? M04Address { get; set; }
    public required string M04Phone { get; set; }
    public required string M04Email { get; set; }
    public string? M04Logo { get; set; }
    public bool M04IsActive { get; set; } = true;
    public Guid? M04CreatedBy { get; set; }
}

public class UpdateRestaurantsDto
{
    public string? M04Name { get; set; }
    public string? M04Address { get; set; }
    public string? M04Phone { get; set; }
    public string? M04Email { get; set; }
    public string? M04Logo { get; set; }
    public bool? M04IsActive { get; set; }
    public Guid? M04UpdatedBy { get; set; }
}
