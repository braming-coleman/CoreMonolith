using CoreMonolith.Application.Access.Users.Register;
using FluentValidation.TestHelper;

namespace CoreMonolith.Application.UnitTests.Access.Users.Register;

public class RegisterUserCommandValidatorTests
{
    private readonly RegisterUserCommandValidator _validator;

    public RegisterUserCommandValidatorTests()
    {
        _validator = new RegisterUserCommandValidator();
    }

    [Fact]
    public void Validator_ShouldFail_WhenFirstNameIsEmpty()
    {
        var command = new RegisterUserCommand("test@example.com", "", "Doe", "password123");
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.FirstName);
    }

    [Fact]
    public void Validator_ShouldFail_WhenEmailIsInvalid()
    {
        var command = new RegisterUserCommand("invalid_email", "John", "Doe", "password123");
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public void Validator_ShouldPass_ForValidCommand()
    {
        var command = new RegisterUserCommand("test@example.com", "John", "Doe", "password123");
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
