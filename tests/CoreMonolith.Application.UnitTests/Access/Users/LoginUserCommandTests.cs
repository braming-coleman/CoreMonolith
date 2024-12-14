using CoreMonolith.Application.Abstractions.Authentication;
using CoreMonolith.Application.Access.Users.Login;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Access;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.Users;

public class LoginUserCommandTests
{
    private static readonly LoginUserCommand Command = new(
        "test@test.com",
        "vahjnousirha)98yc4w4t3409h8tgoipsvhgns");

    private readonly LoginUserCommandHandler _handler;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IPasswordHasher _passwordHasherMock;
    private readonly ITokenProvider _tokenProviderMock;

    public LoginUserCommandTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _passwordHasherMock = Substitute.For<IPasswordHasher>();
        _tokenProviderMock = Substitute.For<ITokenProvider>();

        _handler = new(_unitOfWorkMock, _passwordHasherMock, _tokenProviderMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenUserDoesNotExist()
    {
        //Arrange
        _unitOfWorkMock
            .Access
            .UserRepository
            .GetUserByEmailAsync(Arg.Is<string>(e => e == Command.Email))
            .Returns((User?)null);

        //Act
        var result = await _handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(UserErrors.NotFoundByEmail);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenPAsswordIsIncorrect()
    {
        //Arrange
        _unitOfWorkMock
            .Access
            .UserRepository
            .GetUserByEmailAsync(Arg.Is<string>(e => e == Command.Email))
            .Returns(new User());

        _passwordHasherMock
            .Verify(Arg.Is<string>(e => e == Command.Password), Arg.Any<string>())
            .Returns(false);

        //Act
        var result = await _handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(UserErrors.NotFoundByEmail);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenDetailsAreCorrect()
    {
        //Arrange
        _unitOfWorkMock
            .Access
            .UserRepository
            .GetUserByEmailAsync(Arg.Is<string>(e => e == Command.Email))
            .Returns(new User());

        _passwordHasherMock
            .Verify(Arg.Is<string>(e => e == Command.Password), Arg.Any<string>())
            .Returns(true);

        //Act
        var result = await _handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
