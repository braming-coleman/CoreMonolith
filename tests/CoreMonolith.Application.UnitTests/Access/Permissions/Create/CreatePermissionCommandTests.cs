using CoreMonolith.Application.BusinessLogic.Access.Permissions.Create;
using FluentAssertions;

namespace CoreMonolith.Application.UnitTests.Access.Permissions.Create;

public class CreatePermissionCommandTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var key = "permission-key";
        var description = "permission description";

        // Act
        var command = new CreatePermissionCommand(Guid.CreateVersion7(), key, description);

        // Assert
        command.Key.Should().Be(key);
        command.Description.Should().Be(description);
    }
}