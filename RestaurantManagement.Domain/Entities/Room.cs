namespace RestaurantManagement.Domain.Entities;
public class Room
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