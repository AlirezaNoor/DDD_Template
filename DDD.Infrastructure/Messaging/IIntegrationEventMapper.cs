using DDD.Domain.Common;
using DDD.Infrastructure.Messaging;

namespace DDD.Infrastructure.Messaging;

public interface IIntegrationEventMapper
{
    IEnumerable<IntegrationEventBase> Map(IDomainEvent domainEvent);
}
