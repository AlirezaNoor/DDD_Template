using DDD.Domain.Common;
using DDD.Domain.Entities;

namespace DDD.Domain.Events;

public sealed class CatalogDetailsUpdatedEvent : DomainEventBase
{
    public CatalogId CatalogId { get; }
    public string OldName { get; }
    public string OldDescription { get; }
    public string NewName { get; }
    public string NewDescription { get; }

    public CatalogDetailsUpdatedEvent(CatalogId catalogId, string oldName, string oldDescription, string newName, string newDescription)
    {
        CatalogId = catalogId;
        OldName = oldName;
        OldDescription = oldDescription;
        NewName = newName;
        NewDescription = newDescription;
    }
}
