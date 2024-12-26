using FluentAssertions;
using Modules.UserService.Application.BusinessLogic.Users.GetByEmail;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.Users;
using NSubstitute;

namespace UserService.Application.UnitTests.BusinessLogic.Users;

public class GetByEmailUnitTests
{
    private readonly IUserRepository _userRepo = Substitute.For<IUserRepository>();
    private readonly IUserPermissionGroupRepository _userGroupRepo = Substitute.For<IUserPermissionGroupRepository>();
    private readonly GetUserByEmailQueryHandler _handler;

    public GetByEmailUnitTests()
    {
        _handler = new GetUserByEmailQueryHandler(_userRepo, _userGroupRepo);
    }

    [Fact]
    public async Task Handle_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var email = "test@example.com";
        var query = new GetUserByEmailQuery(email);

        var user = new User
        {
            Id = Guid.CreateVersion7(),
            Email = email
        };
        var permissions = new HashSet<string> { "permission1", "permission2" };

        _userRepo.GetByEmailAsync(email, Arg.Any<CancellationToken>()).Returns(user);
        _userGroupRepo.GetPermissionsByUserId(user.Id, Arg.Any<CancellationToken>()).Returns(permissions);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.User.Should().Be(user);
        result.Value.Permissions.Should().BeEquivalentTo(permissions);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var email = "test@example.com";
        var query = new GetUserByEmailQuery(email);

        _userRepo.GetByEmailAsync(email, Arg.Any<CancellationToken>()).Returns((User)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(UserErrors.NotFoundByEmail(email).Code);
    }

    [Fact]
    public async Task Handle_ShouldNotCallGetPermissionsByUserId_WhenUserDoesNotExist()
    {
        // Arrange
        var email = "test@example.com";
        var query = new GetUserByEmailQuery(email);

        _userRepo.GetByEmailAsync(email, Arg.Any<CancellationToken>()).Returns((User)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        await _userGroupRepo.DidNotReceive().GetPermissionsByUserId(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }
}
