using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities;

[Table("vouchers")]
public class Voucher
{
    [Column("m03_id")]
    [Key]
    public Guid M03Id { get; set; } = Guid.NewGuid();

    [Column("m03_code")]
    public required string M03Code { get; set; }

    [Column("m03_description")]
    public string? M03Description { get; set; }

    [Column("m03_discount_value")]
    public required string M03DiscountValue { get; set; }

    [Column("m03_is_active")]
    public bool M03IsActive { get; set; } = true;

    [Column("m03_min_order_value")]
    public decimal? M03MinOrderValue { get; set; }

    [Column("m03_max_discount")]
    public decimal? M03MaxDiscount { get; set; } = 0.1m;

    [Column("m03_from_date")]
    public DateTime M03FromDate { get; set; }

    [Column("m03_to_date")]
    public DateTime M03ToDate { get; set; }

    [Column("m03_usage_limit")]
    public int M03UsageLimit { get; set; } = 10;

    [Column("m03_restaurant_id")]
    public Guid M03RestaurantId { get; set; }

    [Column("m03_created_at")]
    public DateTime? M03CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("m03_created_by")]
    public Guid? M03CreatedBy { get; set; }

    [Column("m03_updated_at")]
    public DateTime? M03UpdatedAt { get; set; }

    [Column("m03_updated_by")]
    public Guid? M03UpdatedBy { get; set; }
}