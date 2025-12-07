using System.Reflection;
using DDD.Domain.Common;
using DDD.Domain.Interfaces;
using DDD.Infrastructure.Messaging;
using DDD.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly DomainEventDispatcher _domainEventDispatcher;
    private readonly IIntegrationEventMapper _integrationEventMapper;

    public DbSet<DDD.Domain.Entities.Catalog> Catalogs { get; set; }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        DomainEventDispatcher domainEventDispatcher,
        IIntegrationEventMapper integrationEventMapper) : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
        _integrationEventMapper = integrationEventMapper;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await PrepareOutboxMessages(cancellationToken);

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(cancellationToken);

        return result;
    }

    private async Task DispatchEvents(CancellationToken cancellationToken)
    {
        var entitiesWithEvents = ChangeTracker.Entries<IAggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        await _domainEventDispatcher.DispatchAndClearEvents(entitiesWithEvents, cancellationToken);
    }

    private Task PrepareOutboxMessages(CancellationToken cancellationToken)
    {
        var entitiesWithEvents = ChangeTracker.Entries<IAggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        var outbox = Set<OutboxMessage>();

        foreach (var entity in entitiesWithEvents)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                var integrationEvents = _integrationEventMapper.Map(domainEvent);
                foreach (var ie in integrationEvents)
                {
                    var msg = OutboxMessage.Create(ie, ie.OccurredOn);
                    outbox.Add(msg);
                }
            }
        }

        return Task.CompletedTask;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
         await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        await Database.RollbackTransactionAsync(cancellationToken);
    }
}
