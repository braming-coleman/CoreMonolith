using CoreMonolith.Domain.Models.Access.Permissions;
using FluentAssertions;

namespace CoreMonolith.Domain.UnitTests.Models.Access;

public class PermissionCreatedDomainEventTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var @event = new PermissionCreatedDomainEvent(id);

        // Assert
        @event.Id.Should().Be(id);
    }
}