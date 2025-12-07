namespace DDD.Domain.Common;

public interface IBaseEntity
{
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
    void MarkAsDeleted();
}
