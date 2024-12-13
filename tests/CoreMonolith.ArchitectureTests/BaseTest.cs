using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Access;
using CoreMonolith.Infrastructure.Database;
using CoreMonolith.WebApi;
using System.Reflection;

namespace CoreMonolith.ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(User).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}
