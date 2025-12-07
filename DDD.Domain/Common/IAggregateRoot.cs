namespace DDD.Domain.Common;

public interface IAggregateRoot : IBaseEntity
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
