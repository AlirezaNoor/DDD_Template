using MediatR;

namespace DDD.Domain.Common;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}