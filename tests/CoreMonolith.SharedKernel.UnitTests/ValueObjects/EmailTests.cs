using CoreMonolith.SharedKernel.Errors;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentAssertions;

namespace CoreMonolith.SharedKernel.UnitTests.ValueObjects;

public class EmailTests
{
    [Theory]
    [InlineData("test@example.com")]
    [InlineData("user.name+tag@domain.co.uk")]
    public void Create_ValidEmail_ShouldReturnSuccess(string validEmail)
    {
        // Act
        var result = Email.Create(validEmail);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(validEmail);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_EmptyEmail_ShouldReturnFailure(string invalidEmail)
    {
        // Act
        var result = Email.Create(invalidEmail);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Error.ArgumentNull("email"));
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("test@.com")]
    [InlineData("@example.com")]
    public void Create_InvalidEmail_ShouldReturnFailure(string invalidEmail)
    {
        // Act
        var result = Email.Create(invalidEmail);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EmailError.FormatFailure(invalidEmail));
    }

    [Fact]
    public void Equals_SameEmails_ShouldReturnTrue()
    {
        // Arrange
        var email1 = Email.Create("test@example.com").Value;
        var email2 = Email.Create("test@example.com").Value;

        // Act & Assert
        email1.Equals(email2).Should().BeTrue();
    }

    [Fact]
    public void Equals_DifferentEmails_ShouldReturnFalse()
    {
        // Arrange
        var email1 = Email.Create("test@example.com").Value;
        var email2 = Email.Create("other@example.com").Value;

        // Act & Assert
        email1.Equals(email2).Should().BeFalse();
        (email1 != email2).Should().BeTrue();
    }

    [Fact]
    public void GetAtomicValues_ShouldReturnEmailValue()
    {
        // Arrange
        var email = Email.Create("test@example.com").Value;

        // Act
        var values = email.GetAtomicValues();

        // Assert
        values.Should().ContainSingle().Which.Should().Be("test@example.com");
    }
}
