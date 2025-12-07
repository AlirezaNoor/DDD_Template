using DDD.Infrastructure.Messaging;

namespace DDD.Infrastructure.Messaging.Events;

public sealed class CatalogItemAddedIntegrationEvent : IntegrationEventBase
{
    public Guid CatalogId { get; }
    public Guid ItemId { get; }
    public string Name { get; }
    public decimal Amount { get; }
    public string Currency { get; }

    public CatalogItemAddedIntegrationEvent(Guid catalogId, Guid itemId, string name, decimal amount, string currency, DateTime occurredOn)
        : base(occurredOn)
    {
        CatalogId = catalogId;
        ItemId = itemId;
        Name = name;
        Amount = amount;
        Currency = currency;
    }
}
