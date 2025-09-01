using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities;

[Table("categories")]
public class Categories
{
    [Column("m06_id")]
    [Key]
    public int M06Id { get; set; }

    [Column("m06_name")]
    public required string M06Name { get; set; }

    [Column("m06_description")]
    public string? M06Description { get; set; }

    [Column("m06_is_active")]
    public bool M06IsActive { get; set; } = true;

    [Column("m06_restaurant_id")]
    public Guid M06RestaurantId { get; set; }

    [Column("m06_created_at")]
    public DateTime? M06CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("m06_created_by")]
    public Guid? M06CreatedBy { get; set; }

    [Column("m06_updated_at")]
    public DateTime? M06UpdatedAt { get; set; }

    [Column("m06_updated_by")]
    public Guid? M06UpdatedBy { get; set; }
}