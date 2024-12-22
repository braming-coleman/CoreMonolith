using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.SharedKernel.Constants;
using MediatR;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Logging;

namespace CoreMonolith.Application.BusinessLogic.Access.Users.AuthCallback;

internal sealed class UserUpdatedFromKeycloakDomainEventHandler(
    IOutputCacheStore _cacheStore,
    ILogger<UserUpdatedFromKeycloakDomainEventHandler> _logger)
    : INotificationHandler<UserUpdatedDomainEvent>
{
    public async Task Handle(UserUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning("User updated - {notification}", notification);

        await _cacheStore.EvictByTagAsync(Tags.User, cancellationToken);
    }
}
