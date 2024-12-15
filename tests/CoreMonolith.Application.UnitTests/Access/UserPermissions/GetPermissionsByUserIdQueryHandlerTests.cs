using CoreMonolith.Application.Access.UserPermissions.GetPermissionsByUserId;
using CoreMonolith.Domain.Abstractions.Repositories;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.UserPermissions;

public class GetPermissionsByUserIdQueryHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly GetPermissionsByUserIdQueryHandler _handler;

    public GetPermissionsByUserIdQueryHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new GetPermissionsByUserIdQueryHandler(_unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldReturnPermissions_WhenUserHasPermissions()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetPermissionsByUserIdQuery(userId);
        var expectedPermissions = new HashSet<string> { "Read", "Write", "Execute" };

        _unitOfWork.Access.UserPermissionRepository
            .GetPermissionsByUserIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns(expectedPermissions);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expectedPermissions);

        await _unitOfWork.Access.UserPermissionRepository
            .Received(1)
            .GetPermissionsByUserIdAsync(userId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyPermissions_WhenUserHasNoPermissions()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetPermissionsByUserIdQuery(userId);
        var expectedPermissions = new HashSet<string>();

        _unitOfWork.Access.UserPermissionRepository
            .GetPermissionsByUserIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns(expectedPermissions);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();

        await _unitOfWork.Access.UserPermissionRepository
            .Received(1)
            .GetPermissionsByUserIdAsync(userId, Arg.Any<CancellationToken>());
    }
}
