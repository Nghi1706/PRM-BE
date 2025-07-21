namespace RestaurantManagement.Domain.Entities;

public class OrderDetail
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid DishId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Guid StatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedByUser { get; set; }
}