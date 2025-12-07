using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DDD.Infrastructure.Persistence.Extensions;

public static class EntityEntryExtensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        // بررسی اینکه آیا Child Entities (Owned Types) تغییر کرده‌اند
        return entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (
                r.TargetEntry.State == EntityState.Added ||
                r.TargetEntry.State == EntityState.Modified
            )
        );
    }
}