namespace DDD.Domain.Common;

public abstract class DomainEventBase : IDomainEvent
{
    public DateTime OccurredOn { get; }

    protected DomainEventBase()
    {
        OccurredOn = DateTime.UtcNow;
    }
}
