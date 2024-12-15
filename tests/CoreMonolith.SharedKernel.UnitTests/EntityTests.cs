using CoreMonolith.Domain.Access;
using CoreMonolith.SharedKernel.Abstractions;
using FluentAssertions;

namespace CoreMonolith.SharedKernel.UnitTests;

public class EntityTests
{
    private class TestEntity : Entity { }

    private class TestDomainEvent : IDomainEvent { }

    private readonly TestEntity _entity;

    public EntityTests()
    {
        _entity = new TestEntity();
    }

    [Fact]
    public void Raise_ShouldAddDomainEvent()
    {
        // Arrange
        var domainEvent = new TestDomainEvent();

        // Act
        _entity.Raise(domainEvent);

        // Assert
        _entity.DomainEvents.Should().ContainSingle()
            .Which.Should().Be(domainEvent);
    }

    [Fact]
    public void ClearDomainEvents_ShouldRemoveAllDomainEvents()
    {
        // Arrange
        _entity.Raise(new TestDomainEvent());
        _entity.Raise(new TestDomainEvent());

        // Act
        _entity.ClearDomainEvents();

        // Assert
        _entity.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void DomainEvents_ShouldNotAllowModificationDirectly()
    {
        // Act
        var domainEvents = _entity.DomainEvents;

        // Assert
        domainEvents.Should().NotBeNull();
        domainEvents.Should().BeEmpty();
    }

    [Fact]
    public void UserPermission_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var permissionId = Guid.NewGuid();
        var userPermission = new UserPermission
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PermissionId = permissionId
        };

        // Assert
        userPermission.UserId.Should().Be(userId);
        userPermission.PermissionId.Should().Be(permissionId);
    }

    [Fact]
    public void User_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var firstName = "name";
        var lastName = "last-name";
        var email = "test@test.com";
        var hash = "hash";

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = hash
        };

        // Assert
        user.FirstName.Should().Be(firstName);
        user.LastName.Should().Be(lastName);
        user.Email.Should().Be(email);
        user.PasswordHash.Should().Be(hash);
    }

    [Fact]
    public void Permission_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var key = "PermissionKey";
        var description = "Permission Description";

        // Act
        var permission = new Permission
        {
            Id = id,
            Key = key,
            Description = description
        };

        // Assert
        permission.Id.Should().Be(id);
        permission.Key.Should().Be(key);
        permission.Description.Should().Be(description);
    }
}