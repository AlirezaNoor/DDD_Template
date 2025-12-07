using DDD.Domain.Common;

namespace DDD.Domain.Entities;

public readonly record struct CatalogItemId(Guid Value) : IStronglyTypedId
{
    public static CatalogItemId New() => new(Guid.NewGuid());
    public static CatalogItemId Empty => new(Guid.Empty);
    public bool Equals(IStronglyTypedId? other) => other is not null && Value == other.Value;
}
