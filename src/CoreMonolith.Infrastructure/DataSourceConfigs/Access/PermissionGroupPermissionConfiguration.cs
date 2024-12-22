using CoreMonolith.Domain.Models.Access.PermissionGroupPermissions;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreMonolith.Infrastructure.DataSourceConfigs.Access;

internal sealed class PermissionGroupPermissionConfiguration : IEntityTypeConfiguration<PermissionGroupPermission>
{
    public void Configure(EntityTypeBuilder<PermissionGroupPermission> builder)
    {
        builder.ToTable("permission_group_permissions", Schemas.Access);

        builder.HasKey(u => u.Id);

        builder
            .HasIndex(u => new { u.PermissionId, u.PermissionGroupId })
            .IsUnique();

        builder.HasOne(o => o.Permission).WithMany(m => m.PermissionGroupPermissions).HasForeignKey(t => t.PermissionId);
        builder.HasOne(o => o.PermissionGroup).WithMany(m => m.PermissionGroupPermissions).HasForeignKey(t => t.PermissionGroupId);
    }
}
