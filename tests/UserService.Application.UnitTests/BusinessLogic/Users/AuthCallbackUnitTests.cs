using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.SharedKernel.Constants;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Logging;
using Modules.UserService.Application.BusinessLogic.Users.AuthCallback;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.PermissionGroups;
using Modules.UserService.Domain.Models.UserPermissionGroups;
using Modules.UserService.Domain.Models.Users;
using NSubstitute;

namespace Modules.UserService.UnitTests.Application.BusinessLogic.Users;

public class AuthCallbackUnitTests
{
    private readonly IUserRepository _userRepo = Substitute.For<IUserRepository>();
    private readonly IPermissionGroupRepository _groupRepo = Substitute.For<IPermissionGroupRepository>();
    private readonly IUserPermissionGroupRepository _userGroupRepo = Substitute.For<IUserPermissionGroupRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IOutputCacheStore _cacheStore = Substitute.For<IOutputCacheStore>();
    private readonly ILogger<UserPermissionGroupChangedDomainEventHandler> _permissionGroupLogger = Substitute.For<ILogger<UserPermissionGroupChangedDomainEventHandler>>();
    private readonly ILogger<UserRegisteredFromKeycloakDomainEventHandler> _userRegisteredLogger = Substitute.For<ILogger<UserRegisteredFromKeycloakDomainEventHandler>>();
    private readonly ILogger<UserUpdatedFromKeycloakDomainEventHandler> _userUpdatedLogger = Substitute.For<ILogger<UserUpdatedFromKeycloakDomainEventHandler>>();

    private readonly ProcessKeycloakAuthCallbackCommandHandler _handler;

    public AuthCallbackUnitTests()
    {
        _handler = new ProcessKeycloakAuthCallbackCommandHandler(
            _userRepo,
            _groupRepo,
            _userGroupRepo,
            _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new ProcessKeycloakAuthCallbackCommand(
            Guid.CreateVersion7(),
            "test@example.com",
            "John",
            "Doe");

        var permissionGroup = new PermissionGroup
        {
            Id = Guid.CreateVersion7(),
            Code = ApiPermissionGroups.User
        };

        _userRepo.GetByExternalIdAsync(command.ExternalId, Arg.Any<CancellationToken>()).Returns((User)null);
        _userRepo.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns((User)null);
        _groupRepo.FindByCodeAsync(ApiPermissionGroups.User, Arg.Any<CancellationToken>()).Returns(permissionGroup);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.User.Should().NotBeNull();
        result.Value.User.ExternalId.Should().Be(command.ExternalId);
        result.Value.User.Email.Should().Be(command.Email);
        result.Value.User.FirstName.Should().Be(command.FirstName);
        result.Value.User.LastName.Should().Be(command.LastName);

        await _userRepo.Received(1).AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
        await _userGroupRepo.Received(1).AddAsync(Arg.Any<UserPermissionGroup>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldUpdateUser_WhenUserExistsAndDataHasChanged()
    {
        // Arrange
        var command = new ProcessKeycloakAuthCallbackCommand(
            Guid.CreateVersion7(),
            "test@example.com",
            "Jane",
            "Doe");

        var existingUser = new User
        {
            Id = Guid.CreateVersion7(),
            ExternalId = command.ExternalId,
            Email = "old@example.com",
            FirstName = "Old",
            LastName = "Name"
        };

        _userRepo.GetByExternalIdAsync(command.ExternalId, Arg.Any<CancellationToken>()).Returns(existingUser);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.User.Should().NotBeNull();
        result.Value.User.Id.Should().Be(existingUser.Id);
        result.Value.User.Email.Should().Be(command.Email);
        result.Value.User.FirstName.Should().Be(command.FirstName);
        result.Value.User.LastName.Should().Be(command.LastName);

        _userRepo.Received(1).Update(Arg.Any<User>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldNotUpdateUser_WhenUserExistsAndDataHasNotChanged()
    {
        // Arrange
        var command = new ProcessKeycloakAuthCallbackCommand(
            Guid.CreateVersion7(),
            "test@example.com",
            "John",
            "Doe");

        var existingUser = new User
        {
            Id = Guid.CreateVersion7(),
            ExternalId = command.ExternalId,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName
        };

        _userRepo.GetByExternalIdAsync(command.ExternalId, Arg.Any<CancellationToken>()).Returns(existingUser);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.User.Should().NotBeNull();
        result.Value.User.Id.Should().Be(existingUser.Id);

        _userRepo.DidNotReceive().Update(Arg.Any<User>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPermissionGroupNotFound()
    {
        // Arrange
        var command = new ProcessKeycloakAuthCallbackCommand(
            Guid.CreateVersion7(),
            "test@example.com",
            "John",
            "Doe",
            true); // AdminUser = true

        _userRepo.GetByExternalIdAsync(command.ExternalId, Arg.Any<CancellationToken>()).Returns((User)null);
        _userRepo.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns((User)null);
        _groupRepo.FindByCodeAsync(ApiPermissionGroups.Admin, Arg.Any<CancellationToken>()).Returns((PermissionGroup)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be(PermissionGroupErrors.NotFound(ApiPermissionGroups.Admin).Code);
    }

    [Fact]
    public async Task Handle_ShouldRetrievePermissions_WhenUserExists()
    {
        // Arrange
        var command = new ProcessKeycloakAuthCallbackCommand(
            Guid.CreateVersion7(),
            "test@example.com",
            "John",
            "Doe");

        var existingUser = new User
        {
            Id = Guid.CreateVersion7(),
            ExternalId = command.ExternalId,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName
        };

        var permissions = new HashSet<string> { "permission1", "permission2" };

        _userRepo.GetByExternalIdAsync(command.ExternalId, Arg.Any<CancellationToken>()).Returns(existingUser);
        _userGroupRepo.GetPermissionsByUserId(existingUser.Id, Arg.Any<CancellationToken>()).Returns(permissions);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.User.Should().NotBeNull();
        result.Value.Permissions.Should().BeEquivalentTo(permissions);
    }

    // Domain Event Handler Tests

    [Fact]
    public async Task UserPermissionGroupChangedDomainEventHandler_ShouldEvictCache_WhenPermissionsChange()
    {
        // Arrange
        var handler = new UserPermissionGroupChangedDomainEventHandler(_cacheStore, _permissionGroupLogger);
        var notification = new UserPermissionGroupChangedDomainEvent(ApiPermissionGroups.User);

        // Act
        await handler.Handle(notification, CancellationToken.None);

        // Assert
        await _cacheStore.Received(1).EvictByTagAsync(Tags.UserPermission, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UserRegisteredFromKeycloakDomainEventHandler_ShouldEvictCache_WhenUserRegisters()
    {
        // Arrange
        var handler = new UserRegisteredFromKeycloakDomainEventHandler(_cacheStore, _userRegisteredLogger);
        var notification = new UserRegisteredDomainEvent(Guid.CreateVersion7());

        // Act
        await handler.Handle(notification, CancellationToken.None);

        // Assert
        await _cacheStore.Received(1).EvictByTagAsync(Tags.User, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UserUpdatedFromKeycloakDomainEventHandler_ShouldEvictCache_WhenUserUpdates()
    {
        // Arrange
        var handler = new UserUpdatedFromKeycloakDomainEventHandler(_cacheStore, _userUpdatedLogger);
        var notification = new UserUpdatedDomainEvent(Guid.CreateVersion7());

        // Act
        await handler.Handle(notification, CancellationToken.None);

        // Assert
        await _cacheStore.Received(1).EvictByTagAsync(Tags.User, Arg.Any<CancellationToken>());
    }
}

public class ProcessKeycloakAuthCallbackCommandValidatorTests
{
    private readonly ProcessKeycloakAuthCallbackCommandValidator _validator;

    public ProcessKeycloakAuthCallbackCommandValidatorTests()
    {
        _validator = new ProcessKeycloakAuthCallbackCommandValidator();
    }

    [Fact]
    public void Should_have_error_when_ExternalId_is_empty()
    {
        // Arrange
        var command = new ProcessKeycloakAuthCallbackCommand(Guid.Empty, "test@example.com", "John", "Doe");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ExternalId);
    }

    [Fact]
    public void Should_have_error_when_Email_is_empty()
    {
        // Arrange
        var command = new ProcessKeycloakAuthCallbackCommand(Guid.CreateVersion7(), "", "John", "Doe");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_have_error_when_Email_is_invalid()
    {
        // Arrange
        var command = new ProcessKeycloakAuthCallbackCommand(Guid.CreateVersion7(), "invalid-email", "John", "Doe");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_not_have_error_when_Email_is_valid()
    {
        // Arrange
        var command = new ProcessKeycloakAuthCallbackCommand(Guid.CreateVersion7(), "test@example.com", "John", "Doe");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_have_error_when_FirstName_is_empty()
    {
        // Arrange
        var command = new ProcessKeycloakAuthCallbackCommand(Guid.CreateVersion7(), "test@example.com", "", "Doe");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Should_have_error_when_LastName_is_empty()
    {
        // Arrange
        var command = new ProcessKeycloakAuthCallbackCommand(Guid.CreateVersion7(), "test@example.com", "John", "");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void Should_not_have_errors_when_all_fields_are_valid()
    {
        // Arrange
        var command = new ProcessKeycloakAuthCallbackCommand(Guid.CreateVersion7(), "test@example.com", "John", "Doe");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}