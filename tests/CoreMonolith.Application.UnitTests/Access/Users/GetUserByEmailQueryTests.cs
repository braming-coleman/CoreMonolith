using CoreMonolith.Application.Access.Users.GetByEmail;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Access;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.Users;

public class GetUserByEmailQueryTests
{
    private static readonly GetUserByEmailQuery Query = new(
        "test@test.com");

    private readonly GetUserByEmailQueryHandler _handler;
    private readonly IUnitOfWork _unitOfWorkMock;

    public GetUserByEmailQueryTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new(_unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenUserNotFound()
    {
        //Arrange
        _unitOfWorkMock
            .Access
            .UserRepository
            .GetUserByEmailAsync(Arg.Is<string>(e => e == Query.Email))
            .Returns((User?)null);

        //Act
        var result = await _handler.Handle(Query, default);

        //Assert
        result.Error.Should().Be(UserErrors.NotFoundByEmail);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenUserIsFound()
    {
        //Arrange
        _unitOfWorkMock
            .Access
            .UserRepository
            .GetUserByEmailAsync(Arg.Is<string>(e => e == Query.Email))
            .Returns(new User());

        //Act
        var result = await _handler.Handle(Query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
