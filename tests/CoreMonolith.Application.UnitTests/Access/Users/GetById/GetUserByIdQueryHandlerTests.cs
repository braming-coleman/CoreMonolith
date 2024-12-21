using CoreMonolith.Application.BusinessLogic.Access.Users.GetById;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Models.Access.Users;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.Users.GetById;

public class GetUserByIdQueryHandlerTests
{
    private readonly IUnitOfWork _unitOfWorkMock;

    public GetUserByIdQueryHandlerTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task GetUserByIdQueryHandler_Handle_ReturnsNotFound_WhenUserIsNull()
    {
        // Arrange
        var userId = Guid.CreateVersion7();
        var query = new GetUserByIdQuery(userId);

        _unitOfWorkMock.Access.UserRepository.GetByIdAsync(userId, CancellationToken.None)
            .Returns(Task.FromResult<User?>(null));

        var handler = new GetUserByIdQueryHandler(_unitOfWorkMock);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserErrors.NotFound(userId));
    }

    [Fact]
    public async Task GetUserByIdQueryHandler_Handle_ReturnsUserResponse_WhenUserIsFound()
    {
        // Arrange
        var userId = Guid.CreateVersion7();
        var query = new GetUserByIdQuery(userId);

        var user = new User
        {
            Id = userId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        _unitOfWorkMock.Access.UserRepository.GetByIdAsync(userId, CancellationToken.None)
            .Returns(Task.FromResult<User?>(user));

        var handler = new GetUserByIdQueryHandler(_unitOfWorkMock);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var userResponse = result.Value;
        userResponse.Id.Should().Be(userId);
        userResponse.FirstName.Should().Be("John");
        userResponse.LastName.Should().Be("Doe");
        userResponse.Email.Should().Be("john.doe@example.com");
    }
}