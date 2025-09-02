using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities;

[Table("orders")]
public class Orders
{
    [Column("m09_id")]
    [Key]
    public Guid M09Id { get; set; } = Guid.NewGuid();

    [Column("m09_total_amount")]
    public decimal M09TotalAmount { get; set; }

    [Column("m09_final_amount")]
    public decimal M09FinalAmount { get; set; }

    [Column("m09_payment_type")]
    public int M09PaymentType { get; set; }

    [Column("m09_table_id")]
    public int M09TableId { get; set; }

    [Column("m09_voucher_id")]
    public Guid? M09VoucherId { get; set; }

    [Column("m09_created_at")]
    public DateTime? M09CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("m09_created_by")]
    public Guid? M09CreatedBy { get; set; }

    [Column("m09_updated_at")]
    public DateTime? M09UpdatedAt { get; set; }

    [Column("m09_updated_by")]
    public Guid? M09UpdatedBy { get; set; }
}