namespace RestaurantManagement.Application.DTOs;

public class OrderDetailDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid DishId { get; set; }
    public String DishName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int DishStatusId { get; set; }
    public String DishStatusName { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedByUser { get; set; }
}

public class CreateOrderDetailDto
{
    public Guid OrderId { get; set; }
    public Guid DishId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int DishStatusId { get; set; }
    public Guid? CreatedByUser { get; set; }
}

public class UpdateOrderDetailDto
{
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
    public int? DishStatusId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedByUser { get; set; }
}