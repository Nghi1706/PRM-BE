namespace RestaurantManagement.Application.DTOs;

public class TablesDto
{
    public int M08Id { get; set; }
    public string M08Name { get; set; } = string.Empty;
    public int M08Position { get; set; }
    public bool M08IsActive { get; set; }
    public Guid M08RestaurantId { get; set; }
    public int M08StatusId { get; set; }
    public DateTime? M08CreatedAt { get; set; }
    public Guid? M08CreatedBy { get; set; }
    public DateTime? M08UpdatedAt { get; set; }
    public Guid? M08UpdatedBy { get; set; }
}

public class CreateTablesDto
{
    public required string M08Name { get; set; }
    public int M08Position { get; set; }
    public bool M08IsActive { get; set; } = true;
    public Guid M08RestaurantId { get; set; }
    public int M08StatusId { get; set; }
    public Guid? M08CreatedBy { get; set; }
}

public class UpdateTablesDto
{
    public string? M08Name { get; set; }
    public int? M08Position { get; set; }
    public bool? M08IsActive { get; set; }
    public Guid? M08RestaurantId { get; set; }
    public int? M08StatusId { get; set; }
    public Guid? M08UpdatedBy { get; set; }
}