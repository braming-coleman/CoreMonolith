using CoreMonolith.Domain.Access;
using CoreMonolith.SharedKernel.Constants;
using MediatR;
using Microsoft.AspNetCore.OutputCaching;

namespace CoreMonolith.Application.Access.UserPermissions.Create;

internal sealed class UserPermissionCreatedDomainEventHandler(IOutputCacheStore _cacheStore)
    : INotificationHandler<UserPermissionCreatedDomainEvent>
{
    public async Task Handle(UserPermissionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _cacheStore.EvictByTagAsync(Tags.UserPermission, cancellationToken);
    }
}
