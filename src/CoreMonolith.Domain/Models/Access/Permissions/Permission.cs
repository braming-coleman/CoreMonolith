﻿using CoreMonolith.Domain.Models.Access.UserPermissions;
using CoreMonolith.SharedKernel;

namespace CoreMonolith.Domain.Models.Access.Permissions;

public class Permission : Entity
{
    public Guid Id { get; set; }
    public string? Key { get; set; }
    public string? Description { get; set; }

    public List<UserPermission> UserPermissions { get; set; } = [];
}
