using DDD.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DDD.Application.Features.EventHandlers;

public class CatalogItemAddedEventHandler : INotificationHandler<CatalogItemAddedEvent>
{
    private readonly ILogger<CatalogItemAddedEventHandler> _logger;

    public CatalogItemAddedEventHandler(ILogger<CatalogItemAddedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CatalogItemAddedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Catalog item added: {CatalogId} - {ItemId} - {Name} {Amount} {Currency}",
            notification.CatalogId.Value,
            notification.CatalogItemId.Value,
            notification.Name,
            notification.Amount,
            notification.Currency);
        return Task.CompletedTask;
    }
}
