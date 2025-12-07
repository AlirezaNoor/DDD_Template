using DDD.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DDD.Application.Features.EventHandlers;

public class CatalogDetailsUpdatedEventHandler : INotificationHandler<CatalogDetailsUpdatedEvent>
{
    private readonly ILogger<CatalogDetailsUpdatedEventHandler> _logger;

    public CatalogDetailsUpdatedEventHandler(ILogger<CatalogDetailsUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CatalogDetailsUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Catalog updated: {CatalogId} - {OldName} -> {NewName}",
            notification.CatalogId.Value,
            notification.OldName,
            notification.NewName);
        return Task.CompletedTask;
    }
}
