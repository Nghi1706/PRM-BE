namespace RestaurantManagement.Domain.Entities;

public class Table
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public string Name { get; set; } = default!;
    public int TableStatusId { get; set; }
    public string? Position { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? CreatedAt { get; set; }
    public string? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedByUser { get; set; }
}