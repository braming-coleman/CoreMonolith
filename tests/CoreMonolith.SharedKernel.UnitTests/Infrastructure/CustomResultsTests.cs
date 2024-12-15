using CoreMonolith.SharedKernel.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CoreMonolith.SharedKernel.UnitTests.Infrastructure;

public class CustomResultsTests
{
    [Fact]
    public void Problem_ShouldThrowInvalidOperationException_WhenResultIsSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        Action action = () => CustomResults.Problem(result);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(ErrorType.Failure, "Server failure", "An unexpected error occurred", "https://tools.ietf.org/html/rfc7231#section-6.6.1", 500)]
    [InlineData(ErrorType.Validation, "code", "description", "https://tools.ietf.org/html/rfc7231#section-6.5.1", 400)]
    [InlineData(ErrorType.Problem, "code", "description", "https://tools.ietf.org/html/rfc7231#section-6.5.1", 500)]
    [InlineData(ErrorType.NotFound, "code", "description", "https://tools.ietf.org/html/rfc7231#section-6.5.4", 404)]
    [InlineData(ErrorType.Conflict, "code", "description", "https://tools.ietf.org/html/rfc7231#section-6.5.8", 409)]
    public void Problem_ShouldReturnProblemResult_WithCorrectDetails(
        ErrorType errorType,
        string expectedTitle,
        string expectedDetail,
        string expectedType,
        int expectedStatusCode)
    {
        // Arrange
        var error = new Error("code", "description", errorType);
        var result = Result.Failure(error);

        // Act
        var problemResult = CustomResults.Problem(result);

        // Assert
        var problemDetails = (problemResult as ProblemHttpResult)?.ProblemDetails;
        problemDetails.Should().NotBeNull();
        problemDetails!.Title.Should().Be(expectedTitle);
        problemDetails.Detail.Should().Be(expectedDetail);
        problemDetails.Type.Should().Be(expectedType);
        problemDetails.Status.Should().Be(expectedStatusCode);
    }

    [Fact]
    public void Problem_ShouldReturnProblemResult_WithErrors_WhenResultIsValidationError()
    {
        // Arrange
        var errors = new[]
        {
            Error.Failure("code1", "description1"),
            Error.NotFound("code2", "description2")
        };
        var validationError = new ValidationError(errors);
        var result = Result.Failure(validationError);

        // Act
        var problemResult = CustomResults.Problem(result);

        // Assert
        var problemDetails = (problemResult as ProblemHttpResult)?.ProblemDetails;
        problemDetails.Should().NotBeNull();
        problemDetails!.Extensions.Should().ContainKey("errors");
        problemDetails.Extensions["errors"].Should().BeEquivalentTo(errors);
    }
}
