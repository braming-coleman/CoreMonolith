using CoreMonolith.SharedKernel.Errors;
using FluentAssertions;
using Modules.UserService.Domain.Models.Users;

namespace CoreMonolith.Domain.UnitTests.Models.Access;

public class UserErrorsTests
{
    [Fact]
    public void UserErrors_NotFound_CreatesErrorWithCorrectProperties()
    {
        var userId = Guid.CreateVersion7();

        var error = UserErrors.NotFound(userId);

        error.Code.Should().Be("User.NotFound");
        error.Description.Should().Be($"The user with the Id = '{userId}' was not found.");
        error.Type.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public void UserErrors_NotFoundByEmail_ReturnsPredefinedError()
    {
        var mail = "t@t.com";

        var error = UserErrors.NotFoundByEmail(mail);

        error.Code.Should().Be("User.NotFoundByEmail");
        error.Description.Should().Be($"The user with the specified email '{mail}' was not found.");
        error.Type.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public void UserErrors_EmailNotUnique_ReturnsPredefinedError()
    {
        var error = UserErrors.EmailNotUnique;

        error.Code.Should().Be("User.EmailNotUnique");
        error.Description.Should().Be("The provided email is not unique.");
        error.Type.Should().Be(ErrorType.Conflict);
    }
}
