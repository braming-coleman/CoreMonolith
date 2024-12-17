using CoreMonolith.Application.BusinessLogic.Access.Permissions.Create;
using FluentValidation.TestHelper;

namespace CoreMonolith.Application.UnitTests.Access.Permissions.Create;

public class CreatePermissionCommandValidatorTests
{
    private readonly CreatePermissionCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenKeyIsEmpty()
    {
        // Arrange
        var command = new CreatePermissionCommand("", "description");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Key);
    }

    [Fact]
    public void ShouldHaveError_WhenDescriptionIsEmpty()
    {
        // Arrange
        var command = new CreatePermissionCommand("key", "");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Description);
    }

    [Fact]
    public void ShouldNotHaveError_WhenBothKeyAndDescriptionAreNotEmpty()
    {
        // Arrange
        var command = new CreatePermissionCommand("key", "description");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}