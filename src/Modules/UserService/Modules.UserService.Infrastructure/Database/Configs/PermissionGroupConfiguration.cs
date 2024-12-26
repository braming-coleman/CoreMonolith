using CoreMonolith.SharedKernel.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.UserService.Domain.Models.PermissionGroups;

namespace Modules.UserService.Infrastructure.Database.Configs;

internal sealed class PermissionGroupConfiguration : IEntityTypeConfiguration<PermissionGroup>
{
    public void Configure(EntityTypeBuilder<PermissionGroup> builder)
    {
        builder.ToTable("permission_groups", Schemas.UserService);

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Code).IsUnique();

        builder.HasData(
            new PermissionGroup { Id = AccessSeedDataConstants.PermissionGroupId_Admin, Code = ApiPermissionGroups.Admin, Description = "Contains *all* permissions." },
            new PermissionGroup { Id = AccessSeedDataConstants.PermissionGroupId_User, Code = ApiPermissionGroups.User, Description = "Contains non-admin permissions." });
    }
}
