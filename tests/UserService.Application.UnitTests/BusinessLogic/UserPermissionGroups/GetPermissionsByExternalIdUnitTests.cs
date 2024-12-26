using CoreMonolith.Application.Abstractions.Messaging;
using FluentAssertions;
using Modules.UserService.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByExternalId;
using Modules.UserService.Domain.Abstractions.Repositories;
using NSubstitute;

namespace UserService.Application.UnitTests.BusinessLogic.UserPermissionGroups;

public class GetPermissionsByExternalIdUnitTests
{
    private readonly IUserPermissionGroupRepository _userGroupRepo;
    private readonly IQueryHandler<GetPermissionsByExternalIdQuery, HashSet<string>> _handler;

    public GetPermissionsByExternalIdUnitTests()
    {
        _userGroupRepo = Substitute.For<IUserPermissionGroupRepository>();
        _handler = new GetPermissionsByExternalIdQueryHandler(_userGroupRepo);
    }

    [Fact]
    public async Task Handle_ShouldReturnPermissions_WhenUserExists()
    {
        // Arrange
        var externalId = Guid.CreateVersion7();
        var permissions = new HashSet<string> { "Permission1", "Permission2", "Permission3" };
        var query = new GetPermissionsByExternalIdQuery(externalId);

        _userGroupRepo.GetPermissionsByExternalId(externalId, Arg.Any<CancellationToken>()).Returns(permissions);

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
        var externalId = Guid.CreateVersion7();
        var query = new GetPermissionsByExternalIdQuery(externalId);

        _userGroupRepo.GetPermissionsByExternalId(externalId, Arg.Any<CancellationToken>()).Returns(new HashSet<string>());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
}
