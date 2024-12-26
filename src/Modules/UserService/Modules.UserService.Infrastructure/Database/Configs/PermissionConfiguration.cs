using CoreMonolith.SharedKernel.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.UserService.Domain.Models.Permissions;

namespace Modules.UserService.Infrastructure.Database.Configs;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions", Schemas.UserService);

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Key).IsUnique();

        builder.HasData(
            new Permission { Id = AccessSeedDataConstants.PermissionId_UserRead, Key = ApiPermissions.UserRead, Description = "Read access to [user] resource" },
            new Permission { Id = AccessSeedDataConstants.PermissionId_UserWrite, Key = ApiPermissions.UserWrite, Description = "Write access to [user] resource" },
            new Permission { Id = AccessSeedDataConstants.PermissionId_PermissionGroupRead, Key = ApiPermissions.PermissionGroupRead, Description = "Read access to [permission-group] resource" },
            new Permission { Id = AccessSeedDataConstants.PermissionId_PermissionGroupWrite, Key = ApiPermissions.PermissionGroupWrite, Description = "Write access to [permission-group] resource" },
            new Permission { Id = AccessSeedDataConstants.PermissionId_PermissionRead, Key = ApiPermissions.PermissionRead, Description = "Read access to [permission] resource" },
            new Permission { Id = AccessSeedDataConstants.PermissionId_PermissionWrite, Key = ApiPermissions.PermissionWrite, Description = "Write access to [permission] resource" },
            new Permission { Id = AccessSeedDataConstants.PermissionId_WeatherRead, Key = ApiPermissions.WeatherRead, Description = "Read access to [weather] resource" },
            new Permission { Id = AccessSeedDataConstants.PermissionId_ApiGatewayAccess, Key = ApiPermissions.ApiGatewayAccess, Description = "Access via [api-gateway]" });
    }
}
