using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities;

[Table("status")]
public class Status
{
    [Column("m02_id")]
    [Key]
    public int M02Id { get; set; }

    [Column("m02_name")]
    public required string M02Name { get; set; }

    [Column("m02_for_table")]
    public required string M02ForTable { get; set; }

    [Column("m02_is_active")]
    public bool M02IsActive { get; set; } = true;
}
