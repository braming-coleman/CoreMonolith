using CoreMonolith.Domain.Access;
using CoreMonolith.SharedKernel;
using FluentAssertions;

namespace CoreMonolith.Domain.UnitTests.Access;

public class PermissionTests
{
    [Fact]
    public void Permission_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var permissionId = Guid.NewGuid();
        var key = "PermissionKey";
        var description = "Permission Description";

        // Act
        var permission = new Permission
        {
            Id = permissionId,
            Key = key,
            Description = description,
            UserPermissions =
            [
                new() { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), PermissionId = permissionId }
            ]
        };

        // Assert
        permission.Id.Should().Be(permissionId);
        permission.Key.Should().Be(key);
        permission.Description.Should().Be(description);
        permission.UserPermissions.Should().HaveCount(1);
    }

    [Fact]
    public void PermissionErrorsNotFound_ShouldReturnCorrectErrorForPermissionId()
    {
        // Arrange
        var permissionId = Guid.NewGuid();

        // Act
        var error = PermisionErrors.NotFound(permissionId);

        // Assert
        error.Code.Should().Be("Permision.NotFound");
        error.Description.Should().Be($"The permision with the Id = '{permissionId}' was not found.");
        error.Type.Should().Be(ErrorType.NotFound);
    }
}
