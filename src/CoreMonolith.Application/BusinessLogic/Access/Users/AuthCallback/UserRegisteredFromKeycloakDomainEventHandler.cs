using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.SharedKernel.Constants;
using MediatR;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Logging;

namespace CoreMonolith.Application.BusinessLogic.Access.Users.AuthCallback;

internal sealed class UserRegisteredFromKeycloakDomainEventHandler(
    IOutputCacheStore _cacheStore,
    ILogger<UserRegisteredFromKeycloakDomainEventHandler> _logger)
    : INotificationHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning("User registered - {notification}", notification);

        await _cacheStore.EvictByTagAsync(Tags.User, cancellationToken);
    }
}
