using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.DTOs
{
    public class UsersDto
    {
        public Guid M05Id { get; set; }
        public string M05Name { get; set; } = string.Empty;
        public string M05Email { get; set; } = string.Empty;
        public string M05Phone { get; set; } = string.Empty;
        public string? M05Avatar { get; set; }
        public int M05RoleId { get; set; }
        public Guid M05RestaurantId { get; set; }
        public bool M05IsActive { get; set; }
        public DateTime? M05CreatedAt { get; set; }
        public Guid? M05CreatedBy { get; set; }
        public DateTime? M05UpdatedAt { get; set; }
        public Guid? M05UpdatedBy { get; set; }
    }

    public class CreateUsersDto
    {
        public required string M05Name { get; set; }
        public required string M05Password { get; set; }
        public required string M05Email { get; set; }
        public required string M05Phone { get; set; }
        public string? M05Avatar { get; set; }
        public int M05RoleId { get; set; }
        public Guid M05RestaurantId { get; set; }
        public bool M05IsActive { get; set; } = true;
        public Guid? M05CreatedBy { get; set; }
    }

    public class UpdateUsersDto
    {
        public string? M05Name { get; set; }
        public string? M05Password { get; set; }
        public string? M05Email { get; set; }
        public string? M05Phone { get; set; }
        public string? M05Avatar { get; set; }
        public int? M05RoleId { get; set; }
        public Guid? M05RestaurantId { get; set; }
        public bool? M05IsActive { get; set; }
        public Guid? M05UpdatedBy { get; set; }
    }
}
