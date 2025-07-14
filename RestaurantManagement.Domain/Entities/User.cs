using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required Guid RestaurantId { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required Guid RoleId { get; set; }
        public bool? IsActive { get; set; }
        public required string HashedPW { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedByUser { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedByUser { get; set; }
        }
}
