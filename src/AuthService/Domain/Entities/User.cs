using Common.Domain.Entities;

namespace AuthService.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
    }
}
