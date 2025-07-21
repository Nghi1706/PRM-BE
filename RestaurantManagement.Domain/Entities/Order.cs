namespace RestaurantManagement.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public Guid TableId { get; set; }
    public Guid RoomId { get; set; }
    public Guid? UserId { get; set; }
    public Guid StatusId { get; set; }
    public decimal? TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedByUser { get; set; }
}