using CoreMonolith.Domain.Access;
using CoreMonolith.SharedKernel;
using FluentAssertions;

namespace CoreMonolith.Domain.UnitTests.Access;

public class UserErrorsTests
{
    [Fact]
    public void UserErrors_NotFound_CreatesErrorWithCorrectProperties()
    {
        var userId = Guid.NewGuid();

        var error = UserErrors.NotFound(userId);

        error.Code.Should().Be("Users.NotFound");
        error.Description.Should().Be($"The user with the Id = '{userId}' was not found.");
        error.Type.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public void UserErrors_NotFoundByEmail_ReturnsPredefinedError()
    {
        var error = UserErrors.NotFoundByEmail;

        error.Code.Should().Be("Users.NotFoundByEmail");
        error.Description.Should().Be("The user with the specified email was not found.");
        error.Type.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public void UserErrors_EmailNotUnique_ReturnsPredefinedError()
    {
        var error = UserErrors.EmailNotUnique;

        error.Code.Should().Be("Users.EmailNotUnique");
        error.Description.Should().Be("The provided email is not unique.");
        error.Type.Should().Be(ErrorType.Conflict);
    }
}
