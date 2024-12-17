using CoreMonolith.Application.Abstractions.Authentication;
using CoreMonolith.Application.BusinessLogic.Access.Users.Register;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Models.Access.Users;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.Users.Register;

public class RegisterUserCommandHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _handler = new RegisterUserCommandHandler(_unitOfWork, _passwordHasher);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenEmailIsNotUnique()
    {
        // Arrange
        var command = new RegisterUserCommand("test@example.com", "John", "Doe", "password123");
        _unitOfWork.Access.UserRepository
            .ExistsByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserErrors.EmailNotUnique);

        await _unitOfWork.Access.UserRepository.Received(1)
            .ExistsByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        _passwordHasher.DidNotReceive().Hash(Arg.Any<string>());
    }

    [Fact]
    public async Task Handle_ShouldRegisterUserSuccessfully_WhenEmailIsUnique()
    {
        // Arrange
        var command = new RegisterUserCommand("test@example.com", "John", "Doe", "password123");
        var hashedPassword = "hashed_password123";
        _unitOfWork.Access.UserRepository
            .ExistsByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(false);

        _passwordHasher.Hash(command.Password).Returns(hashedPassword);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();

        await _unitOfWork.Access.UserRepository.Received(1).AddAsync(Arg.Is<User>(u =>
            u.Email == command.Email &&
            u.FirstName == command.FirstName &&
            u.LastName == command.LastName &&
            u.PasswordHash == hashedPassword), Arg.Any<CancellationToken>());

        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

}