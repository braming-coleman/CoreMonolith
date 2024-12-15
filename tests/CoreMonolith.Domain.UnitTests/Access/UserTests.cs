using CoreMonolith.Domain.Access;
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
}
