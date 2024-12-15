using CoreMonolith.Application.Access.Users.GetByEmail;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Access;
using FluentAssertions;
using NSubstitute;

namespace CoreMonolith.Application.UnitTests.Access.Users.GetByEmail;

public class GetUserByEmailQueryHandlerTests
{
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly GetUserByEmailQueryHandler _handler;

    public GetUserByEmailQueryHandlerTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new GetUserByEmailQueryHandler(_unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_UserFound_ReturnsUserResponse()
    {
        // Arrange
        var query = new GetUserByEmailQuery("test@example.com");
        var user = new User { Id = Guid.NewGuid(), Email = "test@example.com" };
        _unitOfWorkMock.Access.UserRepository.GetByEmailAsync(query.Email, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<User?>(user));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(user.Id);
        result.Value.Email.Should().Be(user.Email);
    }

    [Fact]
    public async Task Handle_UserNotFound_ReturnsNotFoundError()
    {
        // Arrange
        var query = new GetUserByEmailQuery("nonexistent@example.com");
        _unitOfWorkMock.Access.UserRepository.GetByEmailAsync(query.Email, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<User?>(null));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserErrors.NotFoundByEmail);
    }
}
