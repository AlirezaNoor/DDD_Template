using DDD.Infrastructure.Messaging;

namespace DDD.Infrastructure.Messaging.Events;

public sealed class CatalogCreatedIntegrationEvent : IntegrationEventBase
{
    public Guid CatalogId { get; }
    public string Name { get; }

    public CatalogCreatedIntegrationEvent(Guid catalogId, string name, DateTime occurredOn) : base(occurredOn)
    {
        CatalogId = catalogId;
        Name = name;
    }
}
