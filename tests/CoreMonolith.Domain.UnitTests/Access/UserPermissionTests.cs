using CoreMonolith.Domain.Access.Permissions;
using CoreMonolith.Domain.Access.UserPermissions;
using CoreMonolith.Domain.Access.Users;
using CoreMonolith.SharedKernel;
using FluentAssertions;

namespace CoreMonolith.Domain.UnitTests.Access;

public class UserPermissionTests
{
    [Fact]
    public void UserPermission_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var permissionId = Guid.NewGuid();

        // Act
        var userPermission = new UserPermission
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PermissionId = permissionId,
            User = new User { Id = userId, Email = "test@example.com" },
            Permission = new Permission { Id = permissionId, Key = "PermissionKey" }
        };

        // Assert
        userPermission.UserId.Should().Be(userId);
        userPermission.PermissionId.Should().Be(permissionId);
        userPermission.User.Should().NotBeNull();
        userPermission.User!.Email.Should().Be("test@example.com");
        userPermission.Permission.Should().NotBeNull();
        userPermission.Permission!.Key.Should().Be("PermissionKey");
    }

    [Fact]
    public void UserPermissionErrors_NotFound_ShouldReturnCorrectError()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var error = UserPermisionErrors.NotFound(id);

        // Assert
        error.Code.Should().Be("UserPermision.NotFound");
        error.Description.Should().Be($"The user-permision with the Id = '{id}' was not found.");
        error.Type.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public void UserPermissionErrors_ExistsByUserAndPermissionId_ShouldHaveCorrectProperties()
    {
        // Act
        var error = UserPermisionErrors.ExistsByUserAndPermissionId;

        // Assert
        error.Code.Should().Be("UserPermision.ExistsByUserAndPermissionId");
        error.Description.Should().Be("This UserId and PermissionId combination already exists.");
        error.Type.Should().Be(ErrorType.NotFound);
    }

}
