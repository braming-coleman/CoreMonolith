using CoreMonolith.Application.Abstractions.Authentication;
using CoreMonolith.Application.Access.Users.Register;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Access;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.Users;

public class RegisterUserCommandTests
{
    private static readonly RegisterUserCommand Command = new(
        "test@test.com",
        "Brian",
        "Someguy",
        "vahjnousirha)98yc4w4t3409h8tgoipsvhgns");

    private readonly RegisterUserCommandHandler _handler;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IPasswordHasher _passwordHasherMock;

    public RegisterUserCommandTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _passwordHasherMock = Substitute.For<IPasswordHasher>();

        _handler = new(_unitOfWorkMock, _passwordHasherMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenEmailIsNotUnique()
    {
        //Arrange
        _unitOfWorkMock
            .Access
            .UserRepository
            .UserExistsByEmailAsync(Arg.Is<string>(e => e == Command.Email))
            .Returns(true);

        //Act
        var result = await _handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(UserErrors.EmailNotUnique);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenEmailIsInvalid()
    {
        //Arrange
        var invalidCommand = Command with { Email = "invalid_email" };

        //Act
        var result = await new RegisterUserCommandValidator().ValidateAsync(invalidCommand);

        //Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenEmailIsUnique()
    {
        //Arrange
        _unitOfWorkMock
            .Access
            .UserRepository
            .UserExistsByEmailAsync(Arg.Is<string>(e => e == Command.Email))
            .Returns(false);

        //Act
        var result = await _handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallAddAsync_WhenEmailIsUnique()
    {
        //Arrange
        _unitOfWorkMock
            .Access
            .UserRepository
            .UserExistsByEmailAsync(Arg.Is<string>(e => e == Command.Email))
            .Returns(false);

        //Act
        var result = await _handler.Handle(Command, default);

        //Assert
        await _unitOfWorkMock
            .Access
            .UserRepository
            .Received(1)
            .AddAsync(Arg.Is<User>(u => u.Id == result.Value));
    }

    [Fact]
    public async Task Handle_Should_CallSaveChangesAsynce_WhenEmailIsUnique()
    {
        //Arrange
        _unitOfWorkMock
            .Access
            .UserRepository
            .UserExistsByEmailAsync(Arg.Is<string>(e => e == Command.Email))
            .Returns(false);

        //Act
        await _handler.Handle(Command, default);

        //Assert
        await _unitOfWorkMock
            .Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}


//Arrange

//Act

//Assert