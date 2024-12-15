﻿using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.Access.Permissions.Create;

public sealed record CreatePermissionCommand(string Key, string Description) : ICommand<Guid>;
