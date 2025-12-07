using DDD.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DDD.Infrastructure.Services;

public class DomainEventDispatcher
{
    private readonly IPublisher _mediator;
    private readonly ILogger<DomainEventDispatcher> _logger;

    public DomainEventDispatcher(IPublisher mediator, ILogger<DomainEventDispatcher> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task DispatchAndClearEvents(IEnumerable<IAggregateRoot> entitiesWithEvents, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                _logger.LogInformation("Dispatching domain event: {Event}", domainEvent.GetType().Name);
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}