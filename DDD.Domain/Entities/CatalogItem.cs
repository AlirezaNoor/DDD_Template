using DDD.Domain.Common;
using DDD.Domain.ValueObjects;

namespace DDD.Domain.Entities;

public class CatalogItem : BaseEntity<CatalogItemId>
{
    public string Name { get; private set; }
    public Price Price { get; private set; }
    public CatalogId CatalogId { get; private set; }

    internal CatalogItem(CatalogItemId id, CatalogId catalogId, string name, Price price) : base(id)
    {
        CatalogId = catalogId;
        Name = name;
        Price = price;
    }

    // Only accessible via Aggregate Root
    internal void UpdatePrice(Price newPrice)
    {
        Price = newPrice;
    }
}
