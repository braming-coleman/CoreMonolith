using CoreMonolith.Application.Access.Users.GetById;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Access;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.Users.GetById;

public class GetUserByIdQueryTests
{
    private static readonly GetUserByIdQuery Query = new(Guid.NewGuid());

    private readonly GetUserByIdQueryHandler _handler;
    private readonly IUnitOfWork _unitOfWorkMock;

    public GetUserByIdQueryTests()
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
            .GetByIdAsync(Arg.Is<Guid>(e => e == Query.UserId))
            .Returns((User?)null);

        //Act
        var result = await _handler.Handle(Query, default);

        //Assert
        result.Error.Should().Be(UserErrors.NotFound(Query.UserId));
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenUserIsFound()
    {
        //Arrange
        _unitOfWorkMock
            .Access
            .UserRepository
            .GetByIdAsync(Arg.Is<Guid>(e => e == Query.UserId))
            .Returns(new User());

        //Act
        var result = await _handler.Handle(Query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
