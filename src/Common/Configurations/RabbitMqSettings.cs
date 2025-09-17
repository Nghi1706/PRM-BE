using System;

namespace Common.Configurations
{
    public class RabbitMqSettings
    {
        public required string Exchange { get; set; }
        public required string RoutingKey { get; set; }
        public required string Queue { get; set; }
        public int MaxRetries { get; set; } = 3;
    }
}