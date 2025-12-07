using System.Linq.Expressions;
using DDD.Domain.Common;

namespace DDD.Domain.Interfaces;

public interface IRepository<TEntity, TId> where TEntity : AggregateRoot<TId> where TId : struct, IStronglyTypedId
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}