namespace RestaurantManagement.Application.DTOs;
public class RoomDto
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedByUser { get; set; }
}

public class CreateRoomDto
{
    public Guid RestaurantId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? CreatedByUser { get; set; }
}

public class UpdateRoomDto
{
    public Guid? RestaurantId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
    public string? UpdatedByUser { get; set; }
}