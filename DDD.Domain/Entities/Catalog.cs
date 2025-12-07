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

        var catalog = new Catalog(CatalogId.New(), name, description);
        catalog.AddDomainEvent(new DDD.Domain.Events.CatalogCreatedEvent(catalog.Id, catalog.Name));
        return catalog;
    }

    public void UpdateDetails(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        var oldName = Name;
        var oldDescription = Description;

        Name = name;
        Description = description;

        AddDomainEvent(new DDD.Domain.Events.CatalogDetailsUpdatedEvent(Id, oldName, oldDescription, Name, Description));
    }

    public void AddItem(string name, decimal priceAmount, string currency)
    {
        var price = Price.Create(priceAmount, currency);
        var item = new CatalogItem(CatalogItemId.New(), Id, name, price);
        _items.Add(item);
        AddDomainEvent(new DDD.Domain.Events.CatalogItemAddedEvent(Id, item.Id, item.Name, item.Price.Amount, item.Price.Currency));
    }
}
