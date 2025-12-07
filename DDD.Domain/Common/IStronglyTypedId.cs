namespace DDD.Domain.Common;

public interface IStronglyTypedId : IEquatable<IStronglyTypedId>
{
    Guid Value { get; }
}
