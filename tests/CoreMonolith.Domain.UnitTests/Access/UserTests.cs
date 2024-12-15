using CoreMonolith.Domain.Access;
using CoreMonolith.SharedKernel;
using FluentAssertions;

namespace CoreMonolith.Domain.UnitTests.Access;

public class UserTests
{
    [Fact]
    public void User_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            PasswordHash = "hashed_password"
        };

        // Assert
        user.Id.Should().NotBeEmpty();
        user.Email.Should().Be("test@example.com");
        user.FirstName.Should().Be("John");
        user.LastName.Should().Be("Doe");
        user.PasswordHash.Should().Be("hashed_password");
    }

    [Fact]
    public void UserErrors_NotFound_ShouldGenerateCorrectErrorForUserId()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var error = UserErrors.NotFound(userId);

        // Assert
        error.Code.Should().Be("Users.NotFound");
        error.Description.Should().Be($"The user with the Id = '{userId}' was not found.");
        error.Type.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public void UserErrors_NotFoundByEmail_ShouldHaveCorrectProperties()
    {
        // Act
        var error = UserErrors.NotFoundByEmail;

        // Assert
        error.Code.Should().Be("Users.NotFoundByEmail");
        error.Description.Should().Be("The user with the specified email was not found.");
        error.Type.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public void UserErrors_EmailNotUnique_ShouldHaveCorrectProperties()
    {
        // Act
        var error = UserErrors.EmailNotUnique;

        // Assert
        error.Code.Should().Be("Users.EmailNotUnique");
        error.Description.Should().Be("The provided email is not unique.");
        error.Type.Should().Be(ErrorType.Conflict);
    }

}
