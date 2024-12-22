using CoreMonolith.SharedKernel.Errors;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentAssertions;

namespace CoreMonolith.SharedKernel.UnitTests.ValueObjects;

public class ResultTests
{
    [Fact]
    public void Success_ShouldReturnSuccessResult()
    {
        // Act
        var result = Result.Success();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().Be(Error.None);
    }

    [Fact]
    public void Success_WithTValue_ShouldReturnSuccessResult()
    {
        // Act
        var result = Result.Success("value");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().Be(Error.None);
        result.Value.Should().Be("value");
    }

    [Fact]
    public void Failure_ShouldReturnFailureResult()
    {
        // Arrange
        var error = Error.Failure("code", "description");

        // Act
        var result = Result.Failure(error);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void Failure_WithTValue_ShouldReturnFailureResult()
    {
        // Arrange
        var error = Error.Failure("code", "description");

        // Act
        var result = Result.Failure<string>(error);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnSuccessResult()
    {
        // Act
        Result<string> result = "value";

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("value");
    }

    [Fact]
    public void ValidationFailure_ShouldReturnFailureResult()
    {
        // Arrange
        var error = Error.Failure("code", "description");

        // Act
        var result = Result<string>.ValidationFailure(error);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenIsSuccessAndErrorIsNotNone()
    {
        // Act
        Action action = () => new Result(true, Error.Failure("code", "description"));

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Invalid error*");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenIsNotSuccessAndErrorIsNone()
    {
        // Act
        Action action = () => new Result(false, Error.None);

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Invalid error*");
    }

    [Fact]
    public void Value_ShouldThrowInvalidOperationException_WhenAccessingValueOnFailureResult()
    {
        // Arrange
        var result = Result.Failure<string>(Error.Failure("code", "description"));

        // Act
        Action action = () => { var value = result.Value; };

        // Assert
        action.Should().Throw<InvalidOperationException>().WithMessage("The value of a failure result can't be accessed.");
    }
}
