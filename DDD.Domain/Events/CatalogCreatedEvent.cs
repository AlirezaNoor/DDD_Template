using DDD.Domain.Common;
using DDD.Domain.Entities;

namespace DDD.Domain.Events;

public sealed class CatalogCreatedEvent : DomainEventBase
{
    public CatalogId CatalogId { get; }
    public string Name { get; }

    public CatalogCreatedEvent(CatalogId catalogId, string name)
    {
        CatalogId = catalogId;
        Name = name;
    }
}
