using DDD.Domain.Common;
using DDD.Domain.ValueObjects;

namespace DDD.Domain.Entities;

public class Catalog : AggregateRoot<CatalogId>
{
    private readonly List<CatalogItem> _items = new();
    public IReadOnlyCollection<CatalogItem> Items => _items.AsReadOnly();

    public string Name { get; private set; }
    public string Description { get; private set; }

    private Catalog(CatalogId id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static Catalog Create(string name, string description)
    {
        // Enforce invariants here
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        return new Catalog(CatalogId.New(), name, description);
    }

    public void UpdateDetails(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        Name = name;
        Description = description;
    }

    public void AddItem(string name, decimal priceAmount, string currency)
    {
        var price = Price.Create(priceAmount, currency);
        var item = new CatalogItem(CatalogItemId.New(), Id, name, price);
        _items.Add(item);
    }
}
