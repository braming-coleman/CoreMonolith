using CoreMonolith.Domain.Access.Permissions;
using CoreMonolith.SharedKernel;
using FluentAssertions;

namespace CoreMonolith.Domain.UnitTests.Access;

public class PermisionErrorsTests
{
    [Fact]
    public void NotFound_ShouldReturnNotFoundErrorWithCorrectDetails()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var error = PermissionErrors.NotFound(id);

        // Assert
        error.Should().Be(new Error("Permission.NotFound", $"The permission with the Id = '{id}' was not found.", ErrorType.NotFound));
    }

    [Fact]
    public void ExistsByKey_ShouldReturnConflictErrorWithCorrectDetails()
    {
        // Act
        var error = PermissionErrors.ExistsByKey;

        // Assert
        error.Should().Be(new Error("Permission.ExistsByKey", "Duplicate permission key.", ErrorType.Conflict));
    }
}