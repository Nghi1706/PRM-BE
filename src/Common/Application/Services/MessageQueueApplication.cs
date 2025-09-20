using Common.Application.Interfaces;
using Common.Domain.Interfaces;

namespace Common.Application.Services
{
    public class MessageQueueApplication : IMessageQueueApplication
    {
        private readonly IMessageQueueService _messageQueueService;

        public MessageQueueApplication(IMessageQueueService messageQueueService)
        {
            _messageQueueService = messageQueueService;
        }

        public async Task PublishMessageAsync<T>(string exchange, string routingKey, T message)
        {
            await _messageQueueService.PublishAsync(exchange, routingKey, message);
        }

        public async Task SubscribeToQueueAsync<T>(string exchangeName, string routingKey, string queueName, Func<T, Task> messageHandler)
        {
            await _messageQueueService.SubscribeAsync(exchangeName, routingKey, queueName, messageHandler);
        }

        public async Task<bool> CheckConnectionAsync()
        {
            return await _messageQueueService.IsConnectedAsync();
        }
    }
}
