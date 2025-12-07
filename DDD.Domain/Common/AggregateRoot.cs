namespace DDD.Domain.Common;

public abstract class AggregateRoot<TId> : BaseEntity<TId>, IAggregateRoot where TId : struct, IStronglyTypedId
{
    protected AggregateRoot(TId id) : base(id) { }
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}