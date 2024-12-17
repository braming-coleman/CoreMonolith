using CoreMonolith.Application.BusinessLogic.Access.UserPermissions.Create;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Models.Access.Permissions;
using CoreMonolith.Domain.Models.Access.UserPermissions;
using CoreMonolith.Domain.Models.Access.Users;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.UserPermissions.Create;

public class CreateUserPermissionCommandHandlerTests
{
    private static readonly Guid UserId = Guid.NewGuid();
    private static readonly Guid PermissionId = Guid.NewGuid();

    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateUserPermissionCommandHandler _handler;

    public CreateUserPermissionCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new CreateUserPermissionCommandHandler(_unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new CreateUserPermissionCommand(Guid.NewGuid(), Guid.NewGuid());
        _unitOfWork.Access.UserRepository
            .ExistsByIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserErrors.NotFound(command.UserId));
    }


    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPermissionDoesNotExist()
    {
        // Arrange
        var command = new CreateUserPermissionCommand(Guid.NewGuid(), Guid.NewGuid());
        _unitOfWork.Access.UserRepository.ExistsByIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(true);
        _unitOfWork.Access.PermissionRepository.ExistsByIdAsync(command.PermissionId, Arg.Any<CancellationToken>())
            .Returns(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(PermissionErrors.NotFound(command.PermissionId));
    }


    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserPermissionAlreadyExists()
    {
        // Arrange
        var command = new CreateUserPermissionCommand(Guid.NewGuid(), Guid.NewGuid());
        _unitOfWork.Access.UserRepository.ExistsByIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(true);
        _unitOfWork.Access.PermissionRepository.ExistsByIdAsync(command.PermissionId, Arg.Any<CancellationToken>())
            .Returns(true);
        _unitOfWork.Access.UserPermissionRepository.ExistsByUserAndPermissionIdAsync(command.UserId, command.PermissionId, Arg.Any<CancellationToken>())
            .Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserPermisionErrors.ExistsByUserAndPermissionId);
    }



    [Fact]
    public async Task Handle_ShouldCreateUserPermission_WhenAllChecksPass()
    {
        // Arrange
        var command = new CreateUserPermissionCommand(Guid.NewGuid(), Guid.NewGuid());
        _unitOfWork.Access.UserRepository.ExistsByIdAsync(command.UserId, Arg.Any<CancellationToken>()).Returns(true);
        _unitOfWork.Access.PermissionRepository.ExistsByIdAsync(command.PermissionId, Arg.Any<CancellationToken>()).Returns(true);
        _unitOfWork.Access.UserPermissionRepository.ExistsByUserAndPermissionIdAsync(command.UserId, command.PermissionId, Arg.Any<CancellationToken>()).Returns(false);

        var userPermissionId = Guid.NewGuid();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _unitOfWork.Access.UserPermissionRepository.Received(1).AddAsync(Arg.Is<UserPermission>(up =>
            up.UserId == command.UserId &&
            up.PermissionId == command.PermissionId
        ), Arg.Any<CancellationToken>());

        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
