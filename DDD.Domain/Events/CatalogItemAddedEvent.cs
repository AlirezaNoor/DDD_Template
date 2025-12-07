using DDD.Domain.Common;
using DDD.Domain.Entities;

namespace DDD.Domain.Events;

public sealed class CatalogItemAddedEvent : DomainEventBase
{
    public CatalogId CatalogId { get; }
    public CatalogItemId CatalogItemId { get; }
    public string Name { get; }
    public decimal Amount { get; }
    public string Currency { get; }

    public CatalogItemAddedEvent(CatalogId catalogId, CatalogItemId catalogItemId, string name, decimal amount, string currency)
    {
        CatalogId = catalogId;
        CatalogItemId = catalogItemId;
        Name = name;
        Amount = amount;
        Currency = currency;
    }
}
