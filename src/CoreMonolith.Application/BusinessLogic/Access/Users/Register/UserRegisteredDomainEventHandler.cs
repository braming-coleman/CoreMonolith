﻿using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.SharedKernel.Constants;
using MediatR;
using Microsoft.AspNetCore.OutputCaching;

namespace CoreMonolith.Application.BusinessLogic.Access.Users.Register;

internal sealed class UserRegisteredDomainEventHandler(IOutputCacheStore _cacheStore)
    : INotificationHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        await _cacheStore.EvictByTagAsync(Tags.User, cancellationToken);
    }
}
