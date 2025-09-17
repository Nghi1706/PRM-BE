namespace AuthService.Domain.Entities
{
    public class LoginResponse
    {
        public required string Token { get; set; }
        public required UserInfo User { get; set; }
    }

    public class UserInfo
    {
        public required string Username { get; set; }
        public required string Role { get; set; }
    }
}
