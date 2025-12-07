namespace DDD.Infrastructure.Messaging;

public interface IMessageBroker
{
    Task PublishAsync(string type, string payload, CancellationToken cancellationToken = default);
}
