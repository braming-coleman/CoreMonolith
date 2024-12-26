using FluentAssertions;
using Modules.UserService.Domain.Models.Permissions;

namespace CoreMonolith.Domain.UnitTests.Models.Access;

public class PermissionTests
{
    [Fact]
    public void Permission_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var permissionId = Guid.CreateVersion7();
        var key = "PermissionKey";
        var description = "Permission Description";

        // Act
        var permission = new Permission
        {
            Id = permissionId,
            Key = key,
            Description = description,
            PermissionGroupPermissions =
            [
                new() { Id = Guid.CreateVersion7(), PermissionGroupId = Guid.CreateVersion7(), PermissionId = permissionId }
            ]
        };

        // Assert
        permission.Id.Should().Be(permissionId);
        permission.Key.Should().Be(key);
        permission.Description.Should().Be(description);
        permission.PermissionGroupPermissions.Should().HaveCount(1);
    }
}
