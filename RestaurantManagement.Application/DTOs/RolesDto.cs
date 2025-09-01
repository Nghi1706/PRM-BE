namespace RestaurantManagement.Application.DTOs
{
    public class RolesDto
    {
        public int M01Id { get; set; }
        public string M01Name { get; set; } = string.Empty;
        public string? M01Description { get; set; }
        public bool M01IsActive { get; set; }
    }

    public class CreateRolesDto
    {
        public required string M01Name { get; set; }
        public string? M01Description { get; set; }
        public bool M01IsActive { get; set; } = true;
    }

    public class UpdateRolesDto
    {
        public string? M01Name { get; set; }
        public string? M01Description { get; set; }
        public bool? M01IsActive { get; set; }
    }
}