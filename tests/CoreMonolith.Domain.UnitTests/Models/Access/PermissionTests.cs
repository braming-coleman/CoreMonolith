using CoreMonolith.Domain.Models.Access.Permissions;
using FluentAssertions;

namespace CoreMonolith.Domain.UnitTests.Models.Access;

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
}
