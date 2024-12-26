using CoreMonolith.Domain.Abstractions.Messaging;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.UserService.Domain.Models.Users;
using Modules.UserService.Infrastructure.Database;
using NSubstitute;

namespace CoreMonolith.Infrastructure.UnitTests.Database;

public class UserServiceDbContextTests
{
    [Fact]
    public async Task SaveChangesAsync_ShouldPublishDomainEvents()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<UserServiceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var publisher = Substitute.For<IPublisher>();
        var dbContext = new UserServiceDbContext(options, publisher);

        var user = new User { Email = "test@example.com", FirstName = "first", LastName = "last" };
        user.Raise(Substitute.For<IDomainEvent>());
        dbContext.Users.Add(user);

        // Act
        await dbContext.SaveChangesAsync();

        // Assert
        await publisher.Received(1).Publish(Arg.Any<IDomainEvent>());
    }

    [Fact]
    public void OnModelCreating_ShouldApplyConfigurationsFromAssembly()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<UserServiceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var publisher = Substitute.For<IPublisher>();
        var dbContext = new UserServiceDbContext(options, publisher);
        var modelBuilder = Substitute.For<ModelBuilder>();

        // Act
        dbContext.OnModelCreatingInternal(modelBuilder);

        // Assert
        modelBuilder.Received(1).ApplyConfigurationsFromAssembly(Arg.Any<System.Reflection.Assembly>());
    }

    [Fact]
    public void OnModelCreating_ShouldSetDefaultSchema()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<UserServiceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var publisher = Substitute.For<IPublisher>();
        var dbContext = new UserServiceDbContext(options, publisher);
        var modelBuilder = Substitute.For<ModelBuilder>();

        // Act
        dbContext.OnModelCreatingInternal(modelBuilder);

        // Assert
        modelBuilder.Received(1).HasDefaultSchema(Arg.Any<string>());
    }

    [Fact]
    public void DbSetProperties_ShouldNotBeNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<UserServiceDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var publisher = Substitute.For<IPublisher>();
        var dbContext = new UserServiceDbContext(options, publisher);

        // Assert
        dbContext.Users.Should().NotBeNull();
        dbContext.Permissions.Should().NotBeNull();
    }
}
