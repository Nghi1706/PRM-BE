namespace Common.Configurations
{
    public class MessageQueueSettings
    {
        public string Provider { get; set; } = "RabbitMQ";
        public required RabbitMqSettings RabbitMQ { get; set; }
    }
}
