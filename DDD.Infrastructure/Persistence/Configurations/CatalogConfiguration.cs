using DDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Infrastructure.Persistence.Configurations;

public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                id => id.Value,
                value => new CatalogId(value));

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.OwnsMany(c => c.Items, items =>
        {
            items.ToTable("CatalogItems");
            
            items.HasKey(i => i.Id);
            
            items.Property(i => i.Id)
                .HasConversion(
                    id => id.Value,
                    value => new CatalogItemId(value));

            items.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            items.OwnsOne(i => i.Price, price =>
            {
                price.Property(p => p.Amount).HasColumnName("PriceAmount").HasColumnType("decimal(18,2)");
                price.Property(p => p.Currency).HasColumnName("PriceCurrency").HasMaxLength(3);
            });
            
            items.WithOwner().HasForeignKey("CatalogId");
        });
    }
}
