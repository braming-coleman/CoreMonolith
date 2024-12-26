using CoreMonolith.Application.Abstractions.Messaging;
using FluentAssertions;
using Modules.UserService.Application.BusinessLogic.Users;
using Modules.UserService.Application.BusinessLogic.Users.GetById;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.Users;
using NSubstitute;

namespace UserService.Application.UnitTests.BusinessLogic.Users;

public class GetByIdUnitTests
{
    private readonly IUserRepository _userRepo;
    private readonly IUserPermissionGroupRepository _userGroupRepo;
    private readonly IQueryHandler<GetUserByIdQuery, UserResult> _handler;

    public GetByIdUnitTests()
    {
        _userRepo = Substitute.For<IUserRepository>();
        _userGroupRepo = Substitute.For<IUserPermissionGroupRepository>();
        _handler = new GetUserByIdQueryHandler(_userRepo, _userGroupRepo);
    }

    [Fact]
    public async Task Handle_ShouldReturnUserAndPermissions_WhenUserExists()
    {
        // Arrange
        var userId = Guid.CreateVersion7();
        var user = new User { Id = userId, Email = "test@example.com" };
        var permissions = new HashSet<string> { "Permission1", "Permission2" };
        var query = new GetUserByIdQuery(userId);

        _userRepo.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns(user);
        _userGroupRepo.GetPermissionsByUserId(userId, Arg.Any<CancellationToken>()).Returns(permissions);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.User.Should().Be(user);
        result.Value.Permissions.Should().BeEquivalentTo(permissions);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.CreateVersion7();
        var query = new GetUserByIdQuery(userId);

        _userRepo.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns((User)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserErrors.NotFound(userId));
    }

    [Fact]
    public async Task Handle_ShouldNotCallGetPermissionsByUserId_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.CreateVersion7();
        var query = new GetUserByIdQuery(userId);

        _userRepo.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns((User)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        await _userGroupRepo.DidNotReceive().GetPermissionsByUserId(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }
}
