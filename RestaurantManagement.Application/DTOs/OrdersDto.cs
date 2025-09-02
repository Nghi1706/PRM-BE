namespace RestaurantManagement.Application.DTOs;

public class OrdersDto
{
    public Guid M09Id { get; set; }
    public decimal M09TotalAmount { get; set; }
    public decimal M09FinalAmount { get; set; }
    public int M09PaymentType { get; set; }
    public int M09TableId { get; set; }
    public Guid? M09VoucherId { get; set; }
    public DateTime? M09CreatedAt { get; set; }
    public Guid? M09CreatedBy { get; set; }
    public DateTime? M09UpdatedAt { get; set; }
    public Guid? M09UpdatedBy { get; set; }
}

public class CreateOrdersDto
{
    public required decimal M09TotalAmount { get; set; }
    public required decimal M09FinalAmount { get; set; }
    public required int M09PaymentType { get; set; }
    public required int M09TableId { get; set; }
    public Guid? M09VoucherId { get; set; }
    public Guid? M09CreatedBy { get; set; }
}

public class UpdateOrdersDto
{
    public decimal? M09TotalAmount { get; set; }
    public decimal? M09FinalAmount { get; set; }
    public int? M09PaymentType { get; set; }
    public int? M09TableId { get; set; }
    public Guid? M09VoucherId { get; set; }
    public Guid? M09UpdatedBy { get; set; }
}