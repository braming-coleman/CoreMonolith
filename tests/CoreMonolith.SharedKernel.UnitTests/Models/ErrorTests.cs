﻿using FluentAssertions;

namespace CoreMonolith.SharedKernel.UnitTests.Models;

public class ErrorTests
{
    [Fact]
    public void None_ShouldHaveEmptyCodeAndDescription()
    {
        // Act
        var error = Error.None;

        // Assert
        error.Code.Should().Be(string.Empty);
        error.Description.Should().Be(string.Empty);
        error.Type.Should().Be(ErrorType.Failure);
    }

    [Fact]
    public void NullValue_ShouldHaveCorrectCodeAndDescription()
    {
        // Act
        var error = Error.NullValue;

        // Assert
        error.Code.Should().Be("General.Null");
        error.Description.Should().Be("Null value was provided");
        error.Type.Should().Be(ErrorType.Failure);
    }

    [Fact]
    public void Failure_ShouldCreateFailureError()
    {
        // Act
        var error = Error.Failure("Test.Code", "Test description");

        // Assert
        error.Code.Should().Be("Test.Code");
        error.Description.Should().Be("Test description");
        error.Type.Should().Be(ErrorType.Failure);
    }

    [Fact]
    public void NotFound_ShouldCreateNotFoundError()
    {
        // Act
        var error = Error.NotFound("Test.NotFound", "Resource not found");

        // Assert
        error.Code.Should().Be("Test.NotFound");
        error.Description.Should().Be("Resource not found");
        error.Type.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public void Conflict_ShouldCreateConflictError()
    {
        // Act
        var error = Error.Conflict("Test.Conflict", "Conflict occurred");

        // Assert
        error.Code.Should().Be("Test.Conflict");
        error.Description.Should().Be("Conflict occurred");
        error.Type.Should().Be(ErrorType.Conflict);
    }
}
