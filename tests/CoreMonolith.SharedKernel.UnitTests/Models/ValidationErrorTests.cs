using CoreMonolith.SharedKernel.Errors;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentAssertions;

namespace CoreMonolith.SharedKernel.UnitTests.Models;

public class ValidationErrorTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var errors = new[]
        {
            Error.Failure("code1", "description1"),
            Error.NotFound("code2", "description2")
        };

        // Act
        var validationError = new ValidationError(errors);

        // Assert
        validationError.Code.Should().Be("Validation.General");
        validationError.Description.Should().Be("One or more validation errors occurred");
        validationError.Type.Should().Be(ErrorType.Validation);
        validationError.Errors.Should().BeEquivalentTo(errors);
    }

    [Fact]
    public void FromResults_ShouldReturnValidationErrorWithFailureResults()
    {
        // Arrange
        var results = new[]
        {
            Result.Success(),
            Result.Failure(Error.Failure("code1", "description1")),
            Result.Success<string>("value"),
            Result.Failure<int>(Error.NotFound("code2", "description2"))
        };

        // Act
        var validationError = ValidationError.FromResults(results);

        // Assert
        validationError.Errors.Should().HaveCount(2);
        validationError.Errors.Should().Contain(e => e.Code == "code1");
        validationError.Errors.Should().Contain(e => e.Code == "code2");
    }
}
