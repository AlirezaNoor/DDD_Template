using DDD.Domain.Common;

namespace DDD.Domain.Entities;

public readonly record struct CatalogId(Guid Value) : IStronglyTypedId
{
    public static CatalogId New() => new(Guid.NewGuid());
    public static CatalogId Empty => new(Guid.Empty);
    public bool Equals(IStronglyTypedId? other) => other is not null && Value == other.Value;
}
