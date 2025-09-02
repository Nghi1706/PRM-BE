namespace RestaurantManagement.Application.DTOs;

public class OrderDetailsDto
{
    public Guid M10Id { get; set; }
    public int M10Quantity { get; set; }
    public decimal M10Price { get; set; }
    public decimal M10TotalAmount { get; set; }
    public string M10PaymentStatus { get; set; } = string.Empty;
    public int M10StatusId { get; set; }
    public Guid M10OrderId { get; set; }
    public int M10DishId { get; set; }
    public DateTime? M10CreatedAt { get; set; }
    public Guid? M10CreatedBy { get; set; }
    public DateTime? M10UpdatedAt { get; set; }
    public Guid? M10UpdatedBy { get; set; }
}

public class CreateOrderDetailsDto
{
    public required int M10Quantity { get; set; }
    public required decimal M10Price { get; set; }
    public required decimal M10TotalAmount { get; set; }
    public string M10PaymentStatus { get; set; } = "unpaid";
    public required int M10StatusId { get; set; }
    public required Guid M10OrderId { get; set; }
    public required int M10DishId { get; set; }
    public Guid? M10CreatedBy { get; set; }
}

public class UpdateOrderDetailsDto
{
    public int? M10Quantity { get; set; }
    public decimal? M10Price { get; set; }
    public decimal? M10TotalAmount { get; set; }
    public string? M10PaymentStatus { get; set; }
    public int? M10StatusId { get; set; }
    public Guid? M10OrderId { get; set; }
    public int? M10DishId { get; set; }
    public Guid? M10UpdatedBy { get; set; }
}