using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities;

[Table("restaurants")]
public class Restaurants
{
    [Column("m04_id")]
    [Key]
    public Guid M04Id { get; set; } = Guid.NewGuid();

    [Column("m04_name")]
    public required string M04Name { get; set; }

    [Column("m04_address")]
    public string? M04Address { get; set; }

    [Column("m04_phone")]
    public required string M04Phone { get; set; }

    [Column("m04_email")]
    public required string M04Email { get; set; }

    [Column("m04_logo")]
    public string? M04Logo { get; set; }

    [Column("m04_is_active")]
    public bool M04IsActive { get; set; } = true;

    [Column("m04_created_at")]
    public DateTime? M04CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("m04_created_by")]
    public Guid? M04CreatedBy { get; set; }

    [Column("m04_updated_at")]
    public DateTime? M04UpdatedAt { get; set; }

    [Column("m04_updated_by")]
    public Guid? M04UpdatedBy { get; set; }
}
