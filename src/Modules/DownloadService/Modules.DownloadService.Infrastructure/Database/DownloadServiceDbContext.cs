using CoreMonolith.Domain.Abstractions.Messaging;
using CoreMonolith.Domain.Models;
using CoreMonolith.Domain.Models.Idempotency;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.DownloadService.Application.Abstractions.Data;
using Modules.DownloadService.Domain.Models.DownloadClients;

namespace Modules.DownloadService.Infrastructure.Database;

public sealed class DownloadServiceDbContext(DbContextOptions<DownloadServiceDbContext> options, IPublisher publisher)
    : DbContext(options), IDownloadServiceDbContext
{
    public DbSet<DownloadClient> DownloadClients { get; set; }

    public DbSet<IdempotentRequest> IdempotentRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DownloadServiceDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    internal void OnModelCreatingInternal(ModelBuilder modelBuilder)
    {
        OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // When should you publish domain events?
        //
        // 1. BEFORE calling SaveChangesAsync
        //     - domain events are part of the same transaction
        //     - immediate consistency
        // 2. AFTER calling SaveChangesAsync
        //     - domain events are a separate transaction
        //     - eventual consistency
        //     - handlers can fail

        int result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                List<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach (IDomainEvent domainEvent in domainEvents)
            await publisher.Publish(domainEvent);
    }
}
