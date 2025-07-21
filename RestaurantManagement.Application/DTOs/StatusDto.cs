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
public class CreateStatusDto
{
    public required Guid CategoryId { get; set; }
    public string Name { get; set; } = default!;
    public required int Code { get; set; }
    public string? Description { get; set; }
    public Guid? CreatedByUser { get; set; }
    public bool? IsActive { get; set; }
}
public class UpdateStatusDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Code { get; set; }
    public bool? IsActive { get; set; }
    public Guid? UpdatedByUser { get; set; }
}

