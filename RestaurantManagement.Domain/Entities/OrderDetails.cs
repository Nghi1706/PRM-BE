using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities;

[Table("order_details")]
public class OrderDetails
{
    [Column("m10_id")]
    [Key]
    public Guid M10Id { get; set; } = Guid.NewGuid();

    [Column("m10_quantity")]
    public int M10Quantity { get; set; }

    [Column("m10_price")]
    public decimal M10Price { get; set; }

    [Column("m10_total_amount")]
    public decimal M10TotalAmount { get; set; }

    [Column("m10_payment_status")]
    public string M10PaymentStatus { get; set; } = "unpaid";

    [Column("m10_status_id")]
    public int M10StatusId { get; set; }

    [Column("m10_order_id")]
    public Guid M10OrderId { get; set; }

    [Column("m10_dish_id")]
    public int M10DishId { get; set; }

    [Column("m10_created_at")]
    public DateTime? M10CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("m10_created_by")]
    public Guid? M10CreatedBy { get; set; }

    [Column("m10_updated_at")]
    public DateTime? M10UpdatedAt { get; set; }

    [Column("m10_updated_by")]
    public Guid? M10UpdatedBy { get; set; }
}