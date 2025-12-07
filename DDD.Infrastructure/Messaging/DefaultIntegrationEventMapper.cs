using DDD.Domain.Common;
using DDD.Domain.Entities;
using DDD.Domain.Events;
using DDD.Infrastructure.Messaging.Events;

namespace DDD.Infrastructure.Messaging;

public class DefaultIntegrationEventMapper : IIntegrationEventMapper
{
    public IEnumerable<IntegrationEventBase> Map(IDomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case CatalogCreatedEvent e:
                return new[] { new CatalogCreatedIntegrationEvent(e.CatalogId.Value, e.Name, e.OccurredOn) };
            case CatalogDetailsUpdatedEvent e:
                return new[] { new CatalogDetailsUpdatedIntegrationEvent(e.CatalogId.Value, e.OldName, e.OldDescription, e.NewName, e.NewDescription, e.OccurredOn) };
            case CatalogItemAddedEvent e:
                return new[] { new CatalogItemAddedIntegrationEvent(e.CatalogId.Value, e.CatalogItemId.Value, e.Name, e.Amount, e.Currency, e.OccurredOn) };
            default:
                return Array.Empty<IntegrationEventBase>();
        }
    }
}
