using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    [Table("roles")]
    public class Roles
    {
        [Column("m01_id")]
        [Key]
        public int M01Id { get; set; }
        [Column("m01_name")]
        public required string M01Name { get; set; }
        [Column("m01_description")]

        public string? M01Description { get; set; }

        [Column("m01_is_active")]
        public bool M01IsActive { get; set; }

    }
}
