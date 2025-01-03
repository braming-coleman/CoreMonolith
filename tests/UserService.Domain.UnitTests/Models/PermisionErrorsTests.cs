﻿using CoreMonolith.SharedKernel.Errors;
using FluentAssertions;
using Modules.UserService.Domain.Models.Permissions;

namespace CoreMonolith.Domain.UnitTests.Models.Access;

public class PermisionErrorsTests
{
    [Fact]
    public void NotFound_ShouldReturnNotFoundErrorWithCorrectDetails()
    {
        // Arrange
        var id = Guid.CreateVersion7();

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