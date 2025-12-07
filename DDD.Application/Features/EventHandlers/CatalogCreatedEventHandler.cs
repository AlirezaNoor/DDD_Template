using DDD.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DDD.Application.Features.EventHandlers;

public class CatalogCreatedEventHandler : INotificationHandler<CatalogCreatedEvent>
{
    private readonly ILogger<CatalogCreatedEventHandler> _logger;

    public CatalogCreatedEventHandler(ILogger<CatalogCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CatalogCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Catalog created: {CatalogId} - {Name}", notification.CatalogId.Value, notification.Name);
        return Task.CompletedTask;
    }
}
