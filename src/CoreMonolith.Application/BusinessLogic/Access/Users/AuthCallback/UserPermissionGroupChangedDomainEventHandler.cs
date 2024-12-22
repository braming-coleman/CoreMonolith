using CoreMonolith.Domain.Models.Access.UserPermissionGroups;
using CoreMonolith.SharedKernel.Constants;
using MediatR;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Logging;

namespace CoreMonolith.Application.BusinessLogic.Access.Users.AuthCallback;

internal sealed class UserPermissionGroupChangedDomainEventHandler(
    IOutputCacheStore _cacheStore,
    ILogger<UserPermissionGroupChangedDomainEventHandler> _logger)
    : INotificationHandler<UserPermissionGroupChangedDomainEvent>
{
    public async Task Handle(UserPermissionGroupChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning("User permissions changed - {notification}", notification);

        await _cacheStore.EvictByTagAsync(Tags.UserPermission, cancellationToken);
    }
}
