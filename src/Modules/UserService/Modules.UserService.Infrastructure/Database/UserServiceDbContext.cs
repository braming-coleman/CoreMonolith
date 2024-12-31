using CoreMonolith.Domain.Abstractions.Messaging;
using CoreMonolith.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.UserService.Application.Abstractions.Data;
using Modules.UserService.Domain.Models.PermissionGroupPermissions;
using Modules.UserService.Domain.Models.PermissionGroups;
using Modules.UserService.Domain.Models.Permissions;
using Modules.UserService.Domain.Models.UserPermissionGroups;
using Modules.UserService.Domain.Models.Users;

namespace Modules.UserService.Infrastructure.Database;

public sealed class UserServiceDbContext(DbContextOptions<UserServiceDbContext> options, IPublisher publisher)
    : DbContext(options), IUserServiceDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserPermissionGroup> UserPermissionGroups { get; set; }
    public DbSet<PermissionGroup> PermissionGroups { get; set; }
    public DbSet<PermissionGroupPermission> PermissionGroupPermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserServiceDbContext).Assembly);

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
