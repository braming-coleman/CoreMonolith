using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.SharedKernel.Constants;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Logging;
using Modules.UserService.Application.BusinessLogic.Permissions.Create;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.Permissions;
using NSubstitute;

namespace UserService.Application.UnitTests.BusinessLogic.Permissions;

public class CreateUnitTests
{
    private readonly IPermissionRepository _permRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommandHandler<CreatePermissionCommand, Guid> _handler;

    private readonly IOutputCacheStore _cacheStore;
    private readonly ILogger<PermissionCreatedDomainEventHandler> _logger;
    private readonly PermissionCreatedDomainEventHandler _domainHandler;

    public CreateUnitTests()
    {
        _permRepo = Substitute.For<IPermissionRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new CreatePermissionCommandHandler(_permRepo, _unitOfWork);

        _cacheStore = Substitute.For<IOutputCacheStore>();
        _logger = Substitute.For<ILogger<PermissionCreatedDomainEventHandler>>();
        _domainHandler = new PermissionCreatedDomainEventHandler(_cacheStore, _logger);
    }

    [Fact]
    public async Task Handle_ShouldCreatePermission_WhenKeyDoesNotExist()
    {
        // Arrange
        var command = new CreatePermissionCommand(Guid.CreateVersion7(), "TestKey", "Test Description");

        _permRepo.ExistsByKeyAsync(command.Key, Arg.Any<CancellationToken>()).Returns(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
        await _permRepo.Received(1).AddAsync(Arg.Is<Permission>(p => p.Key == command.Key && p.Description == command.Description), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenKeyAlreadyExists()
    {
        // Arrange
        var command = new CreatePermissionCommand(Guid.CreateVersion7(), "ExistingKey", "Test Description");

        _permRepo.ExistsByKeyAsync(command.Key, Arg.Any<CancellationToken>()).Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(PermissionErrors.ExistsByKey);
        await _permRepo.DidNotReceive().AddAsync(Arg.Any<Permission>(), Arg.Any<CancellationToken>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldEvictPermissionCache_WhenCalled()
    {
        // Arrange
        var notification = new PermissionCreatedDomainEvent(Guid.CreateVersion7());

        // Act
        await _domainHandler.Handle(notification, CancellationToken.None);

        // Assert
        await _cacheStore.Received(1).EvictByTagAsync(Tags.Permission, Arg.Any<CancellationToken>());
    }
}

public class CreatePermissionCommandValidatorTests
{
    private readonly CreatePermissionCommandValidator _validator;

    public CreatePermissionCommandValidatorTests()
    {
        _validator = new CreatePermissionCommandValidator();
    }

    [Fact]
    public void Should_have_error_when_Key_is_empty()
    {
        // Arrange
        var command = new CreatePermissionCommand(Guid.CreateVersion7(), "", "Test Description");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Key);
    }

    [Fact]
    public void Should_have_error_when_Description_is_empty()
    {
        // Arrange
        var command = new CreatePermissionCommand(Guid.CreateVersion7(), "TestKey", "");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Description);
    }

    [Fact]
    public void Should_not_have_errors_when_all_fields_are_valid()
    {
        // Arrange
        var command = new CreatePermissionCommand(Guid.CreateVersion7(), "TestKey", "Test Description");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
