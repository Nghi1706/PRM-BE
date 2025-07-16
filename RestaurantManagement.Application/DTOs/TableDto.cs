namespace RestaurantManagement.Application.DTOs;

public class TableDto
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Guid StatusId { get; set; }
    public string? Name { get; set; }
    public string? Position { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedByUser { get; set; }
}

public class CreateTableDto
{
    public Guid RoomId { get; set; }
    public required string Name { get; set; }
    public string? Position { get; set; }
    public required Guid StatusId { get; set; }
    public string? CreatedByUser { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class UpdateTableDto
{
    public Guid? RoomId { get; set; }
    public string? Name { get; set; }
    public string? Position { get; set; }
    public Guid? StatusId { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedByUser { get; set; }
}