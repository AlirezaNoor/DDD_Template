using DDD.Infrastructure.Messaging;

namespace DDD.Infrastructure.Messaging.Events;

public sealed class CatalogDetailsUpdatedIntegrationEvent : IntegrationEventBase
{
    public Guid CatalogId { get; }
    public string OldName { get; }
    public string OldDescription { get; }
    public string NewName { get; }
    public string NewDescription { get; }

    public CatalogDetailsUpdatedIntegrationEvent(Guid catalogId, string oldName, string oldDescription, string newName, string newDescription, DateTime occurredOn)
        : base(occurredOn)
    {
        CatalogId = catalogId;
        OldName = oldName;
        OldDescription = oldDescription;
        NewName = newName;
        NewDescription = newDescription;
    }
}
