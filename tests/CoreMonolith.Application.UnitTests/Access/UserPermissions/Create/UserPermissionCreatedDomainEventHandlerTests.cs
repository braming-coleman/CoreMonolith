using CoreMonolith.Application.BusinessLogic.Access.UserPermissions.Create;
using CoreMonolith.Domain.Models.Access.UserPermissions;
using CoreMonolith.SharedKernel.Constants;
using Microsoft.AspNetCore.OutputCaching;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.UserPermissions.Create;
public class UserPermissionCreatedDomainEventHandlerTests
{
    private readonly IOutputCacheStore _cacheStore;
    private readonly UserPermissionCreatedDomainEventHandler _handler;

    public UserPermissionCreatedDomainEventHandlerTests()
    {
        _cacheStore = Substitute.For<IOutputCacheStore>();
        _handler = new UserPermissionCreatedDomainEventHandler(_cacheStore);
    }

    [Fact]
    public async Task Handle_ShouldEvictCache_WhenDomainEventHandled()
    {
        // Arrange
        var domainEvent = new UserPermissionCreatedDomainEvent(Guid.CreateVersion7());

        // Act
        await _handler.Handle(domainEvent, CancellationToken.None);

        // Assert
        await _cacheStore.Received(1).EvictByTagAsync(Tags.UserPermission, Arg.Any<CancellationToken>());
    }

}
