using Microsoft.EntityFrameworkCore;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.UserPermissionGroups;
using Modules.UserService.Infrastructure.Database;

namespace Modules.UserService.Infrastructure.Repositories;

public class UserPermissionGroupRepository(UserServiceDbContext _dbContext)
    : UserServiceRepository<UserPermissionGroup>(_dbContext), IUserPermissionGroupRepository
{
    private readonly UserServiceDbContext _dbContext = _dbContext;

    public async Task<HashSet<string>> GetPermissionsByExternalId(Guid externalId, CancellationToken cancellationToken = default)
    {
        var results = await _dbContext.Database
            .SqlQuery<string>($@"
                SELECT 
                    p.key
                FROM user_service.permissions p
                    INNER JOIN user_service.permission_group_permissions pgp 
                        ON p.Id = pgp.permission_id
                    INNER JOIN user_service.user_permission_groups upg 
                        ON pgp.permission_group_id = upg.permission_group_id
                    INNER JOIN user_service.users u 
                        ON upg.user_id = u.id
                WHERE u.external_id = {externalId}")
            .ToHashSetAsync(cancellationToken);

        HashSet<string> permissions = [];

        if (results is not null)
            permissions = results;

        return permissions;
    }

    public async Task<HashSet<string>> GetPermissionsByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        var results = await _dbContext.Database
            .SqlQuery<string>($@"
                SELECT 
                    p.key
                FROM user_service.permissions p
                    INNER JOIN user_service.permission_group_permissions pgp 
                        ON p.Id = pgp.permission_id
                    INNER JOIN user_service.user_permission_groups upg 
                        ON pgp.permission_group_id = upg.permission_group_id
                WHERE upg.user_id = {userId}")
            .ToHashSetAsync(cancellationToken);

        HashSet<string> permissions = [];

        if (results is not null)
            permissions = results;

        return permissions;
    }
}
