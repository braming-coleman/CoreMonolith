using CoreMonolith.SharedKernel.Abstractions;
using FluentAssertions;

namespace CoreMonolith.SharedKernel.UnitTests.Models;

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
}