using DDD.Domain.Common;
using DDD.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DDD.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IDateTime _dateTime;

    public AuditableEntityInterceptor(IDateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<IBaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                // CreatedAt is set in constructor
            }

            if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                // Using reflection for IBaseEntity as we want to support any implementation
                // But IBaseEntity has only getters for UpdatedAt?
                // Wait, IBaseEntity interface defined in previous step only has getters?
                // I need to check if I added setters or if I should cast to implementation?
                // Actually BaseEntity<T> has protected set.
                // EF Core ChangeTracker works fine with properties even if they are readonly in interface
                // IF we map to the backing property name on the concrete class via Entry Property.
                
                entry.Property(nameof(IBaseEntity.UpdatedAt)).CurrentValue = _dateTime.UtcNow;
            }
        }
    }
}

public static class EntityEntryExtensions
{
    public static bool HasChangedOwnedEntities(this Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry) =>
        entry.References.Any(r => 
            r.TargetEntry != null && 
            r.TargetEntry.Metadata.IsOwned() && 
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}