namespace RestaurantManagement.Domain.Entities;

public class OrderDetail
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid DishId { get; set; }
    public string DishName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int DishStatusId { get; set; }
    public string DishStatusName { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedByUser { get; set; }
}