using CoreMonolith.Application.Access.UserPermissions.GetPermissionsByUserId;
using CoreMonolith.Domain.Abstractions.Repositories;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.UserPermissions;

public class GetPermissionsByUserIdQueryTests
{
    private static readonly Guid UserId = Guid.NewGuid();
    private static readonly GetPermissionsByUserIdQuery Query = new(UserId);

    private readonly GetPermissionsByUserIdQueryHandler _handler;
    private readonly IUnitOfWork _unitOfWorkMock;

    public GetPermissionsByUserIdQueryTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new GetPermissionsByUserIdQueryHandler(_unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnEmpty_WhenUserIdIsInvalid()
    {
        //Arrange
        _unitOfWorkMock
            .Access
            .UserPermissionRepository
            .GetPermissionsByUserIdAsync(Arg.Is<Guid>(e => e != Query.UserId))
            .Returns(Task.FromResult(new HashSet<string> { }));

        //Act
        var result = await _handler.Handle(Query with { UserId = Guid.NewGuid() }, default);

        //Assert

        result.Value.Should().Equal(new HashSet<string> { });
    }

    [Fact]
    public async Task Handle_Should_ReturnPermissions_WhenUserIdIsValid()
    {
        //Arrange
        _unitOfWorkMock
            .Access
            .UserPermissionRepository
            .GetPermissionsByUserIdAsync(Arg.Is<Guid>(e => e == Query.UserId))
            .Returns(Task.FromResult(new HashSet<string> { "user:read", "user:write" }));

        //Act
        var result = await _handler.Handle(Query, default);

        //Assert

        result.Value.Should().Equal(new HashSet<string> { "user:read", "user:write" });
    }
}
