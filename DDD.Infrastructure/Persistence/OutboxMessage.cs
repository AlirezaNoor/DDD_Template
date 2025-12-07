using System.Text.Json;

namespace DDD.Infrastructure.Persistence;

public class OutboxMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Type { get; set; } = default!;
    public string Payload { get; set; } = default!;
    public DateTime OccurredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public int RetryCount { get; set; }
    public string? Error { get; set; }

    public static OutboxMessage Create<T>(T integrationEvent, DateTime occurredOn)
    {
        return new OutboxMessage
        {
            Type = typeof(T).FullName!,
            Payload = JsonSerializer.Serialize(integrationEvent),
            OccurredOn = occurredOn
        };
    }
}
