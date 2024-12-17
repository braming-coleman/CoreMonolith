using CoreMonolith.Application.BusinessLogic.Access.Users.Register;
using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.SharedKernel.Constants;
using Microsoft.AspNetCore.OutputCaching;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.Users.Register;

public class UserRegisteredDomainEventHandlerTests
{
    private readonly IOutputCacheStore _cacheStore;
    private readonly UserRegisteredDomainEventHandler _handler;

    public UserRegisteredDomainEventHandlerTests()
    {
        _cacheStore = Substitute.For<IOutputCacheStore>();
        _handler = new UserRegisteredDomainEventHandler(_cacheStore);
    }

    [Fact]
    public async Task Handle_ShouldEvictCacheSuccessfully()
    {
        // Arrange
        var domainEvent = new UserRegisteredDomainEvent(Guid.NewGuid());

        // Act
        await _handler.Handle(domainEvent, CancellationToken.None);

        // Assert
        await _cacheStore.Received(1).EvictByTagAsync(Tags.User, Arg.Any<CancellationToken>());
    }

}
