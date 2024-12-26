using FluentAssertions;
using Modules.UserService.Domain.Models.Users;

namespace CoreMonolith.Domain.UnitTests.Models.Access;

public class UserTests
{
    [Fact]
    public void User_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.CreateVersion7(),
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe"
        };

        // Assert
        user.Id.Should().NotBeEmpty();
        user.Email.Should().Be("test@example.com");
        user.FirstName.Should().Be("John");
        user.LastName.Should().Be("Doe");
    }
}
