using CoreMonolith.Application.BusinessLogic.Access.Permissions.Create;
using CoreMonolith.Domain.Models.Access.Permissions;
using CoreMonolith.SharedKernel.Constants;
using Microsoft.AspNetCore.OutputCaching;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.Permissions.Create;

public class PermissionCreatedDomainEventHandlerTests
{
    [Fact]
    public async Task Handle_ShouldEvictCacheByTag()
    {
        // Arrange
        var cacheStore = Substitute.For<IOutputCacheStore>();
        var handler = new PermissionCreatedDomainEventHandler(cacheStore);
        var @event = new PermissionCreatedDomainEvent(Guid.CreateVersion7());

        // Act
        await handler.Handle(@event, CancellationToken.None);

        // Assert
        await cacheStore.Received(1).EvictByTagAsync(Tags.Permission, CancellationToken.None);
    }
}