namespace DDD.Infrastructure.Messaging;

public abstract class IntegrationEventBase
{
    public DateTime OccurredOn { get; }

    protected IntegrationEventBase(DateTime occurredOn)
    {
        OccurredOn = occurredOn;
    }
}
