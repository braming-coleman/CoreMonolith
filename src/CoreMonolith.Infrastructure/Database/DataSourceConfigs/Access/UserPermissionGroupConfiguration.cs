using CoreMonolith.Domain.Models.Access.UserPermissionGroups;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreMonolith.Infrastructure.DataSourceConfigs.Access;

internal sealed class UserPermissionGroupConfiguration : IEntityTypeConfiguration<UserPermissionGroup>
{
    public void Configure(EntityTypeBuilder<UserPermissionGroup> builder)
    {
        builder.ToTable("user_permission_groups", Schemas.Access);

        builder.HasKey(u => u.Id);

        builder
            .HasIndex(u => new { u.UserId, u.PermissionGroupId })
            .IsUnique();

        builder.HasOne(o => o.User).WithMany(m => m.UserPermissionGroups).HasForeignKey(t => t.UserId);
        builder.HasOne(o => o.PermissionGroup).WithMany(m => m.UserPermissionGroups).HasForeignKey(t => t.PermissionGroupId);
    }
}
