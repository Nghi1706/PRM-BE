using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities;

[Table("dishes")]
public class Dishes
{
    [Column("m07_id")]
    [Key]
    public int M07Id { get; set; }

    [Column("m07_name")]
    public required string M07Name { get; set; }

    [Column("m07_description")]
    public string? M07Description { get; set; }

    [Column("m07_price")]
    public decimal M07Price { get; set; }

    [Column("m07_image")]
    public required string M07Image { get; set; }

    [Column("m07_is_active")]
    public bool M07IsActive { get; set; } = true;

    [Column("m07_category_id")]
    public int M07CategoryId { get; set; }

    [Column("m07_restaurant_id")]
    public Guid M07RestaurantId { get; set; }

    [Column("m07_created_at")]
    public DateTime? M07CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("m07_created_by")]
    public Guid? M07CreatedBy { get; set; }

    [Column("m07_updated_at")]
    public DateTime? M07UpdatedAt { get; set; }

    [Column("m07_updated_by")]
    public Guid? M07UpdatedBy { get; set; }
}