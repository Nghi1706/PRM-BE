namespace Common.Application.Interfaces
{
    public interface IMessageQueueApplication
    {
        Task PublishMessageAsync<T>(string exchange, string routingKey, T message);
        Task SubscribeToQueueAsync<T>(string exchangeName, string routingKey, string queueName, Func<T, Task> messageHandler);
        Task<bool> CheckConnectionAsync();
    }
}
