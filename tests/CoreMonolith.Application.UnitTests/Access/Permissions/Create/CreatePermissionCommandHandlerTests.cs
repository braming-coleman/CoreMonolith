using CoreMonolith.Application.BusinessLogic.Access.Permissions.Create;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Abstractions.Repositories.Access;
using CoreMonolith.Domain.Models.Access.Permissions;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.Permissions.Create;

public class CreatePermissionCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPermissionAlreadyExists()
    {
        // Arrange
        var unitOfWork = Substitute.For<IUnitOfWork>();
        unitOfWork.Access.Returns(Substitute.For<IAccessContainer>());
        unitOfWork.Access.PermissionRepository.Returns(Substitute.For<IPermissionRepository>());
        unitOfWork.Access.PermissionRepository.ExistsByKeyAsync("existing-key", CancellationToken.None).Returns(true);
        var handler = new CreatePermissionCommandHandler(unitOfWork);
        var command = new CreatePermissionCommand("existing-key", "description");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(PermissionErrors.ExistsByKey);
    }

    [Fact]
    public async Task Handle_ShouldCreatePermission_WhenPermissionDoesNotExist()
    {
        // Arrange
        var unitOfWork = Substitute.For<IUnitOfWork>();
        unitOfWork.Access.Returns(Substitute.For<IAccessContainer>());
        unitOfWork.Access.PermissionRepository.Returns(Substitute.For<IPermissionRepository>());
        unitOfWork.Access.PermissionRepository.ExistsByKeyAsync("new-key", CancellationToken.None).Returns(false);
        var handler = new CreatePermissionCommandHandler(unitOfWork);
        var command = new CreatePermissionCommand("new-key", "description");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await unitOfWork.Access.PermissionRepository.Received(1).AddAsync(Arg.Any<Permission>(), CancellationToken.None);
        await unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }
}