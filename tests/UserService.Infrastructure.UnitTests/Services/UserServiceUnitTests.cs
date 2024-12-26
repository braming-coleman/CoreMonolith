using CoreMonolith.SharedKernel.ValueObjects;
using FluentAssertions;
using Mapster;
using MediatR;
using Modules.UserService.Api;
using Modules.UserService.Api.RequestModels;
using Modules.UserService.Api.ResponseModels;
using Modules.UserService.Application.BusinessLogic.Permissions.Create;
using Modules.UserService.Application.BusinessLogic.Permissions.GetAll;
using Modules.UserService.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByExternalId;
using Modules.UserService.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByUserId;
using Modules.UserService.Application.BusinessLogic.Users;
using Modules.UserService.Application.BusinessLogic.Users.AuthCallback;
using Modules.UserService.Application.BusinessLogic.Users.GetByEmail;
using Modules.UserService.Application.BusinessLogic.Users.GetById;
using Modules.UserService.Domain.Models.Permissions;
using Modules.UserService.Domain.Models.Users;
using Modules.UserService.Infrastructure.Services;
using NSubstitute;

namespace UserService.Infrastructure.UnitTests.Services;

public class UserServiceUnitTests
{
    private readonly ISender _sender;
    private readonly IUserServiceApi _userServiceApi;

    public UserServiceUnitTests()
    {
        _sender = Substitute.For<ISender>();
        _userServiceApi = new UserServiceApi(_sender);
    }

    [Fact]
    public async Task AuthenticationCallbackAsync_ShouldSendCommandAndReturnUserResponse_WhenSuccessful()
    {
        // Arrange
        var request = new AuthenticationCallbackRequest(Guid.CreateVersion7(), "test@example.com", "John", "Doe");
        var command = request.Adapt<ProcessKeycloakAuthCallbackCommand>();
        var user = new User { Id = Guid.CreateVersion7(), ExternalId = Guid.CreateVersion7(), Email = "test@example.com" };
        var permissions = new HashSet<string> { "Permission1" };
        var userResult = new UserResult(user, permissions);

        _sender.Send(Arg.Any<ProcessKeycloakAuthCallbackCommand>(), Arg.Any<CancellationToken>()).Returns(userResult);

        // Act
        var result = await _userServiceApi.AuthenticationCallbackAsync(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new UserResponse(user.Id, user.ExternalId, user.Email, permissions));
        await _sender.Received(1).Send(Arg.Is<ProcessKeycloakAuthCallbackCommand>(c => c.ExternalId == request.ExternalId), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task AuthenticationCallbackAsync_ShouldReturnFailure_WhenCommandFails()
    {
        // Arrange
        var request = new AuthenticationCallbackRequest(Guid.CreateVersion7(), "test@example.com", "John", "Doe");
        var error = Result.Failure<UserResult>(UserErrors.CreationFailed);

        _sender.Send(Arg.Any<ProcessKeycloakAuthCallbackCommand>(), Arg.Any<CancellationToken>()).Returns(error);

        // Act
        var result = await _userServiceApi.AuthenticationCallbackAsync(request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error.Error);
    }

    [Fact]
    public async Task PermissionCreateAsync_ShouldSendCommandAndReturnGuid_WhenSuccessful()
    {
        // Arrange
        var requestId = Guid.CreateVersion7();
        var request = new PermissionRequest("TestKey", "Test Description");
        var command = new CreatePermissionCommand(requestId, request.Key, request.Description);
        var permissionId = Guid.CreateVersion7();

        _sender.Send(Arg.Any<CreatePermissionCommand>(), Arg.Any<CancellationToken>()).Returns(permissionId);

        // Act
        var result = await _userServiceApi.PermissionCreateAsync(requestId, request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(permissionId);
        await _sender.Received(1).Send(Arg.Is<CreatePermissionCommand>(c => c.RequestId == requestId && c.Key == request.Key && c.Description == request.Description), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task PermissionCreateAsync_ShouldReturnFailure_WhenCommandFails()
    {
        // Arrange
        var requestId = Guid.CreateVersion7();
        var request = new PermissionRequest("TestKey", "Test Description");
        var error = Result.Failure<Guid>(PermissionErrors.ExistsByKey);

        _sender.Send(Arg.Any<CreatePermissionCommand>(), Arg.Any<CancellationToken>()).Returns(Result.Failure<Guid>(PermissionErrors.ExistsByKey));

        // Act
        var result = await _userServiceApi.PermissionCreateAsync(requestId, request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error.Error);
    }

    [Fact]
    public async Task PermissionsGetAll_ShouldSendQueryAndReturnListOfPermissions_WhenSuccessful()
    {
        // Arrange
        var permissions = new List<PermissionResponse>
        {
            new(Guid.CreateVersion7(), "Key1", "Description1"),
            new(Guid.CreateVersion7(), "Key2", "Description2")
        };

        _sender.Send(Arg.Any<GetAllPermissionsQuery>(), Arg.Any<CancellationToken>()).Returns(permissions);

        // Act
        var result = await _userServiceApi.PermissionsGetAll(CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(permissions);
        await _sender.Received(1).Send(Arg.Any<GetAllPermissionsQuery>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task PermissionsGetByExternalIdAsync_ShouldSendQueryAndReturnHashSetOfPermissions_WhenSuccessful()
    {
        // Arrange
        var externalId = Guid.CreateVersion7();
        var permissions = new HashSet<string> { "Permission1", "Permission2" };

        _sender.Send(Arg.Any<GetPermissionsByExternalIdQuery>(), Arg.Any<CancellationToken>()).Returns(permissions);

        // Act
        var result = await _userServiceApi.PermissionsGetByExternalIdAsync(externalId, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(permissions);
        await _sender.Received(1).Send(Arg.Is<GetPermissionsByExternalIdQuery>(q => q.ExternalId == externalId), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task PermissionsGetByUserIdAsync_ShouldSendQueryAndReturnHashSetOfPermissions_WhenSuccessful()
    {
        // Arrange
        var userId = Guid.CreateVersion7();
        var permissions = new HashSet<string> { "PermissionA", "PermissionB" };

        _sender.Send(Arg.Any<GetPermissionsByUserIdQuery>(), Arg.Any<CancellationToken>()).Returns(permissions);

        // Act
        var result = await _userServiceApi.PermissionsGetByUserIdAsync(userId, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(permissions);
        await _sender.Received(1).Send(Arg.Is<GetPermissionsByUserIdQuery>(q => q.UserId == userId), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UserGetByEmailAsync_ShouldSendQueryAndReturnUserResponse_WhenSuccessful()
    {
        // Arrange
        var email = "test@example.com";
        var user = new User { Id = Guid.CreateVersion7(), ExternalId = Guid.CreateVersion7(), Email = email };
        var permissions = new HashSet<string> { "Permission1" };
        var userResult = new UserResult(user, permissions);

        _sender.Send(Arg.Any<GetUserByEmailQuery>(), Arg.Any<CancellationToken>()).Returns(userResult);

        // Act
        var result = await _userServiceApi.UserGetByEmailAsync(email, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new UserResponse(user.Id, user.ExternalId, user.Email, permissions));
        await _sender.Received(1).Send(Arg.Is<GetUserByEmailQuery>(q => q.Email == email), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UserGetByEmailAsync_ShouldReturnFailure_WhenQueryFails()
    {
        // Arrange
        var email = "test@example.com";
        var error = Result.Failure<UserResult>(UserErrors.NotFoundByEmail(email));

        _sender.Send(Arg.Any<GetUserByEmailQuery>(), Arg.Any<CancellationToken>()).Returns(Result.Failure<UserResult>(UserErrors.NotFoundByEmail(email)));

        // Act
        var result = await _userServiceApi.UserGetByEmailAsync(email, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error.Error);
    }

    [Fact]
    public async Task UserGetByIdAsync_ShouldSendQueryAndReturnUserResponse_WhenSuccessful()
    {
        // Arrange
        var userId = Guid.CreateVersion7();
        var user = new User { Id = userId, ExternalId = Guid.CreateVersion7(), Email = "test@example.com" };
        var permissions = new HashSet<string> { "Permission1" };
        var userResult = new UserResult(user, permissions);

        _sender.Send(Arg.Any<GetUserByIdQuery>(), Arg.Any<CancellationToken>()).Returns(userResult);

        // Act
        var result = await _userServiceApi.UserGetByIdAsync(userId, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new UserResponse(user.Id, user.ExternalId, user.Email, permissions));
        await _sender.Received(1).Send(Arg.Is<GetUserByIdQuery>(q => q.UserId == userId), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UserGetByIdAsync_ShouldReturnFailure_WhenQueryFails()
    {
        // Arrange
        var userId = Guid.CreateVersion7();
        var error = Result.Failure<UserResult>(UserErrors.NotFound(userId));

        _sender.Send(Arg.Any<GetUserByIdQuery>(), Arg.Any<CancellationToken>()).Returns(Result.Failure<UserResult>(UserErrors.NotFound(userId)));

        // Act
        var result = await _userServiceApi.UserGetByIdAsync(userId, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error.Error);
    }
}
