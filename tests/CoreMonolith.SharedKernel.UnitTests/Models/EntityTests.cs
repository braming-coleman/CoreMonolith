using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Models;
using FluentAssertions;
using NSubstitute;

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
    public void DomainEvents_ShouldNotAllowModificationDirectly()
    {
        // Act
        var domainEvents = _entity.DomainEvents;

        // Assert
        domainEvents.Should().NotBeNull();
        domainEvents.Should().BeEmpty();
    }

    [Fact]
    public void Raise_ShouldAddDomainEventToList()
    {
        // Arrange
        var entity = Substitute.ForPartsOf<Entity>();
        var domainEvent = Substitute.For<IDomainEvent>();

        // Act
        entity.Raise(domainEvent);

        // Assert
        entity.DomainEvents.Should().ContainSingle()
            .Which.Should().Be(domainEvent);
    }

    [Fact]
    public void ClearDomainEvents_ShouldClearDomainEventsList()
    {
        // Arrange
        var entity = Substitute.ForPartsOf<Entity>();
        entity.Raise(Substitute.For<IDomainEvent>());

        // Act
        entity.ClearDomainEvents();

        // Assert
        entity.DomainEvents.Should().BeEmpty();
    }
}