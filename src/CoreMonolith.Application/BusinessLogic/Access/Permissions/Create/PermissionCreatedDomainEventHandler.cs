using CoreMonolith.Domain.Models.Access.Permissions;
using CoreMonolith.SharedKernel.Constants;
using MediatR;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Logging;

namespace CoreMonolith.Application.BusinessLogic.Access.Permissions.Create;

internal sealed class PermissionCreatedDomainEventHandler(
    IOutputCacheStore _cacheStore,
    ILogger<PermissionCreatedDomainEventHandler> _logger)
    : INotificationHandler<PermissionCreatedDomainEvent>
{
    public async Task Handle(PermissionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning("Permission created - {notification}", notification);

        await _cacheStore.EvictByTagAsync(Tags.Permission, cancellationToken);
    }
}