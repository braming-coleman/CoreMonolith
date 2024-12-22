using CoreMonolith.Domain.Models.Access.PermissionGroups;
using CoreMonolith.Infrastructure.Database;
using CoreMonolith.SharedKernel.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreMonolith.Infrastructure.DataSourceConfigs.Access;

internal sealed class PermissionGroupConfiguration : IEntityTypeConfiguration<PermissionGroup>
{
    public void Configure(EntityTypeBuilder<PermissionGroup> builder)
    {
        builder.ToTable("permission_groups", Schemas.Access);

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Code).IsUnique();

        builder.HasData(
            new PermissionGroup { Id = AccessSeedDataConstants.PermissionGroupId_Admin, Code = ApiPermissionGroups.Admin, Description = "Contains *all* permissions." },
            new PermissionGroup { Id = AccessSeedDataConstants.PermissionGroupId_User, Code = ApiPermissionGroups.User, Description = "Contains non-admin permissions." });
    }
}
