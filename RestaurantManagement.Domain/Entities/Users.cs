using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities;

[Table("users")]
public class Users
{
    [Column("m05_id")]
    [Key]
    public Guid M05Id { get; set; } = Guid.NewGuid();

    [Column("m05_name")]
    public required string M05Name { get; set; }

    [Column("m05_hashed_pw")]
    public required string M05HashedPW { get; set; }

    [Column("m05_email")]
    public required string M05Email { get; set; }

    [Column("m05_phone")]
    public required string M05Phone { get; set; }

    [Column("m05_avata")]
    public string? M05Avatar { get; set; }

    [Column("m05_role_id")]
    public int M05RoleId { get; set; }

    [Column("m05_restaurant_id")]
    public Guid M05RestaurantId { get; set; }

    [Column("m05_is_active")]
    public bool M05IsActive { get; set; } = true;

    [Column("m05_created_at")]
    public DateTime? M05CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("m05_created_by")]
    public Guid? M05CreatedBy { get; set; }

    [Column("m05_updated_at")]
    public DateTime? M05UpdatedAt { get; set; }

    [Column("m05_updated_by")]
    public Guid? M05UpdatedBy { get; set; }
}
