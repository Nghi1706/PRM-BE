using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities;

[Table("tables")]
public class Tables
{
    [Column("m08_id")]
    [Key]
    public int M08Id { get; set; }

    [Column("m08_name")]
    public required string M08Name { get; set; }

    [Column("m08_position")]
    public int M08Position { get; set; }

    [Column("m08_is_active")]
    public bool M08IsActive { get; set; } = true;

    [Column("m08_restaurant_id")]
    public Guid M08RestaurantId { get; set; }

    [Column("m08_status_id")]
    public int M08StatusId { get; set; }

    [Column("m08_created_at")]
    public DateTime? M08CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("m08_created_by")]
    public Guid? M08CreatedBy { get; set; }

    [Column("m08_updated_at")]
    public DateTime? M08UpdatedAt { get; set; }

    [Column("m08_updated_by")]
    public Guid? M08UpdatedBy { get; set; }
}