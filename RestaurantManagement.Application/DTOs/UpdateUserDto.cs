using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.DTOs
{
    public class UpdateUserDto
    {
        public Guid? RestaurantId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid? RoleId { get; set; }
        public bool? IsActive { get; set; }
        public string? HashedPW { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedByUser { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedByUser { get; set; }

    }
}
