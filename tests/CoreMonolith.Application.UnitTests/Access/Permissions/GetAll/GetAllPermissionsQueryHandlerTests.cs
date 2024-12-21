using CoreMonolith.Application.BusinessLogic.Access.Permissions;
using CoreMonolith.Application.BusinessLogic.Access.Permissions.GetAll;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Models.Access.Permissions;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.Permissions.GetAll;

public class GetAllPermissionsQueryHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly GetAllPermissionsQueryHandler _handler;

    public GetAllPermissionsQueryHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new GetAllPermissionsQueryHandler(_unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllPermissions()
    {
        // Arrange
        var query = new GetAllPermissionsQuery();
        var mockPermissions = new List<Permission>
        {
            new() { Id = Guid.CreateVersion7(), Key = "Permission1", Description = "Description1" },
            new() { Id = Guid.CreateVersion7(), Key = "Permission2", Description = "Description2" }
        };

        _unitOfWork.Access.PermissionRepository
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(mockPermissions);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
        result.Value.Should().ContainEquivalentOf(new PermissionReposnse
        {
            Id = mockPermissions[0].Id,
            Key = "Permission1",
            Description = "Description1"
        });
        result.Value.Should().ContainEquivalentOf(new PermissionReposnse
        {
            Id = mockPermissions[1].Id,
            Key = "Permission2",
            Description = "Description2"
        });

        await _unitOfWork.Access.PermissionRepository.Received(1)
            .GetAllAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoPermissionsExist()
    {
        // Arrange
        var query = new GetAllPermissionsQuery();
        _unitOfWork.Access.PermissionRepository
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns([]);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();

        await _unitOfWork.Access.PermissionRepository.Received(1)
            .GetAllAsync(Arg.Any<CancellationToken>());
    }
}