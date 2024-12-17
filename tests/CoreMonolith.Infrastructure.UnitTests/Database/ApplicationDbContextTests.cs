using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.Infrastructure.Database;
using CoreMonolith.SharedKernel.Abstractions;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace CoreMonolith.Infrastructure.UnitTests.Database;

public class ApplicationDbContextTests
{
    [Fact]
    public async Task SaveChangesAsync_ShouldPublishDomainEvents()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var publisher = Substitute.For<IPublisher>();
        var dbContext = new ApplicationDbContext(options, publisher);

        var user = new User { Email = "test@example.com", PasswordHash = "password-hash" };
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
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var publisher = Substitute.For<IPublisher>();
        var dbContext = new ApplicationDbContext(options, publisher);
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
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var publisher = Substitute.For<IPublisher>();
        var dbContext = new ApplicationDbContext(options, publisher);
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
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var publisher = Substitute.For<IPublisher>();
        var dbContext = new ApplicationDbContext(options, publisher);

        // Assert
        dbContext.Users.Should().NotBeNull();
        dbContext.Permissions.Should().NotBeNull();
        dbContext.UserPermissions.Should().NotBeNull();
    }
}
