using System;

namespace RestaurantManagement.Application.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int AccessTokenExpiryHours { get; set; } = 12;
        public int RefreshTokenExpiryDays { get; set; } = 7;
    }
}