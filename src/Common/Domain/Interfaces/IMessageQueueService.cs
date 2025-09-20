namespace Common.Domain.Interfaces
{
    public interface IMessageQueueService
    {
        Task PublishAsync<T>(string exchange, string routingKey, T message);
        Task SubscribeAsync<T>(string exchangeName, string routingKey, string queueName, Func<T, Task> handler);
        Task<bool> IsConnectedAsync();
        ValueTask DisposeAsync();
    }
}
