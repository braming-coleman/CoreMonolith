using CoreMonolith.Application.BusinessLogic.Access.UserPermissions.GetPermissionsByExternalId;
using CoreMonolith.Domain.Abstractions.Repositories;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.UserPermissions.GetPermissionsByExternalId;

public class GetPermissionsByExternalIdHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPermissionsByExternalIdHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task Handle_ShouldReturnPermissions_WhenUserExists()
    {
        // Arrange
        var externalId = Guid.NewGuid();
        var expectedPermissions = new HashSet<string> { "permission1", "permission2" };

        _unitOfWork.Access.UserPermissionRepository
            .GetPermissionsByExternalIdAsync(externalId, Arg.Any<CancellationToken>())
            .Returns(expectedPermissions);

        var handler = new GetPermissionsByExternalIdHandler(_unitOfWork);
        var query = new GetPermissionsByExternalIdQuery(externalId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expectedPermissions);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserDoesNotExist()
    {
        // Arrange
        var externalId = Guid.NewGuid();
        var expectedPermissions = new HashSet<string>();

        _unitOfWork.Access.UserPermissionRepository
            .GetPermissionsByExternalIdAsync(externalId, Arg.Any<CancellationToken>())
            .Returns(expectedPermissions);


        var handler = new GetPermissionsByExternalIdHandler(_unitOfWork);
        var query = new GetPermissionsByExternalIdQuery(externalId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);


        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();

        await _unitOfWork.Access.UserPermissionRepository
            .Received(1)
            .GetPermissionsByExternalIdAsync(externalId, Arg.Any<CancellationToken>());
    }
}
