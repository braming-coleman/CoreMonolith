using CoreMonolith.Application.Abstractions.Authentication;
using CoreMonolith.Application.BusinessLogic.Access.Users.Login;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Models.Access.Users;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.Users.Login;

public class LoginUserCommandHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenProvider _tokenProvider;
    private readonly LoginUserCommandHandler _handler;

    public LoginUserCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _tokenProvider = Substitute.For<ITokenProvider>();
        _handler = new LoginUserCommandHandler(_unitOfWork, _tokenProvider);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        var command = new LoginUserCommand("test@example.com", "password123");
        _unitOfWork.Access.UserRepository
            .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns((User?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserErrors.NotFoundByEmail);

        await _unitOfWork.Access.UserRepository.Received(1)
            .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        _tokenProvider.DidNotReceive().Create(Arg.Any<User>());
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPasswordIsIncorrect()
    {
        // Arrange
        var command = new LoginUserCommand("test@example.com", "wrongpassword");
        var user = new User
        {
            Id = Guid.CreateVersion7(),
            Email = command.Email,
        };

        _unitOfWork.Access.UserRepository
            .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(user);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserErrors.NotFoundByEmail);

        await _unitOfWork.Access.UserRepository.Received(1)
            .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        _tokenProvider.DidNotReceive().Create(Arg.Any<User>());
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenUserAuthenticated()
    {
        // Arrange
        var command = new LoginUserCommand("test@example.com", "password123");
        var user = new User
        {
            Id = Guid.CreateVersion7(),
            Email = command.Email,
        };

        string generatedToken = "jwt_token";

        _unitOfWork.Access.UserRepository
            .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(user);

        _tokenProvider.Create(user).Returns(generatedToken);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(generatedToken);

        await _unitOfWork.Access.UserRepository.Received(1)
            .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        _tokenProvider.Received(1).Create(user);
    }

}
