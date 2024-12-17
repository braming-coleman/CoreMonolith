using CoreMonolith.Domain.Models.Access.Permissions;
using CoreMonolith.SharedKernel.Constants;
using MediatR;
using Microsoft.AspNetCore.OutputCaching;

namespace CoreMonolith.Application.BusinessLogic.Access.Permissions.Create;

internal sealed class PermissionCreatedDomainEventHandler(IOutputCacheStore cacheStore)
    : INotificationHandler<PermissionCreatedDomainEvent>
{
    public async Task Handle(PermissionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await cacheStore.EvictByTagAsync(Tags.Permission, cancellationToken);
    }
}