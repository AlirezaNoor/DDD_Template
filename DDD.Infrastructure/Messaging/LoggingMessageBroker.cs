using Microsoft.Extensions.Logging;

namespace DDD.Infrastructure.Messaging;

public class LoggingMessageBroker : IMessageBroker
{
    private readonly ILogger<LoggingMessageBroker> _logger;

    public LoggingMessageBroker(ILogger<LoggingMessageBroker> logger)
    {
        _logger = logger;
    }

    public Task PublishAsync(string type, string payload, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Publishing integration event: {Type} Payload: {Payload}", type, payload);
        return Task.CompletedTask;
    }
}
