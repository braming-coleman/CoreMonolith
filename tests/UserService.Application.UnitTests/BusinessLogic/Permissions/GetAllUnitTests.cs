using CoreMonolith.Application.Abstractions.Messaging;
using FluentAssertions;
using Modules.UserService.Api.ResponseModels;
using Modules.UserService.Application.BusinessLogic.Permissions.GetAll;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.Permissions;
using NSubstitute;

namespace UserService.Application.UnitTests.BusinessLogic.Permissions;

public class GetAllUnitTests
{
    private readonly IPermissionRepository _permRepo;
    private readonly IQueryHandler<GetAllPermissionsQuery, List<PermissionResponse>> _handler;

    public GetAllUnitTests()
    {
        _permRepo = Substitute.For<IPermissionRepository>();
        _handler = new GetAllPermissionsQueryHandler(_permRepo);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfPermissions_WhenPermissionsExist()
    {
        // Arrange
        var permissions = new List<Permission>
        {
            new Permission { Id = Guid.CreateVersion7(), Key = "Key1", Description = "Description1" },
            new Permission { Id = Guid.CreateVersion7(), Key = "Key2", Description = "Description2" },
            new Permission { Id = Guid.CreateVersion7(), Key = "Key3", Description = "Description3" }
        };

        _permRepo.GetAllAsync(Arg.Any<CancellationToken>()).Returns(permissions);

        // Act
        var result = await _handler.Handle(new GetAllPermissionsQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(permissions.Count);
        result.Value.Should().BeEquivalentTo(permissions.Select(p => new PermissionResponse(p.Id, p.Key, p.Description)));
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoPermissionsExist()
    {
        // Arrange
        _permRepo.GetAllAsync(Arg.Any<CancellationToken>()).Returns(new List<Permission>());

        // Act
        var result = await _handler.Handle(new GetAllPermissionsQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
}
