using CoreMonolith.Application.Abstractions.Messaging;
using FluentAssertions;
using Modules.UserService.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByUserId;
using Modules.UserService.Domain.Abstractions.Repositories;
using NSubstitute;

namespace UserService.Application.UnitTests.BusinessLogic.UserPermissionGroups;

public class GetPermissionsByUserIdUnitTests
{
    private readonly IUserPermissionGroupRepository _userGroupRepo;
    private readonly IQueryHandler<GetPermissionsByUserIdQuery, HashSet<string>> _handler;

    public GetPermissionsByUserIdUnitTests()
    {
        _userGroupRepo = Substitute.For<IUserPermissionGroupRepository>();
        _handler = new GetPermissionsByUserIdQueryHandler(_userGroupRepo);
    }

    [Fact]
    public async Task Handle_ShouldReturnPermissions_WhenUserExists()
    {
        // Arrange
        var userId = Guid.CreateVersion7();
        var permissions = new HashSet<string> { "PermissionA", "PermissionB", "PermissionC" };
        var query = new GetPermissionsByUserIdQuery(userId);

        _userGroupRepo.GetPermissionsByUserId(userId, Arg.Any<CancellationToken>()).Returns(permissions);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(permissions);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptySet_WhenUserHasNoPermissions()
    {
        // Arrange
        var userId = Guid.CreateVersion7();
        var query = new GetPermissionsByUserIdQuery(userId);

        _userGroupRepo.GetPermissionsByUserId(userId, Arg.Any<CancellationToken>()).Returns(new HashSet<string>());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
}
