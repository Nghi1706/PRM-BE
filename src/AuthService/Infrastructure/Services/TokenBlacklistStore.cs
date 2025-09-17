using AuthService.Domain.Interfaces;
using StackExchange.Redis;

namespace AuthService.Infrastructure.Services
{
    public class TokenBlacklistStore : ITokenBlacklistStore
    {
        private readonly IConfiguration _configuration;

        public TokenBlacklistStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SetBlacklistAsync(string token, DateTimeOffset expiresAt)
        {
            var redisConnectionString = _configuration["ConnectionStrings:RedisConnection"];
            if (string.IsNullOrEmpty(redisConnectionString))
                throw new InvalidOperationException("Redis connection string is not configured.");

            var redis = ConnectionMultiplexer.Connect(redisConnectionString);
            var db = redis.GetDatabase();

            await db.StringSetAsync($"auth:tokens:{token}", "blacklisted", expiresAt - DateTime.UtcNow);
        }
    }
}
