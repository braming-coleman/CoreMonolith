using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.UserService.Domain.Models.UserPermissionGroups;

namespace Modules.UserService.Infrastructure.Database.Configs;

internal sealed class UserPermissionGroupConfiguration : IEntityTypeConfiguration<UserPermissionGroup>
{
    public void Configure(EntityTypeBuilder<UserPermissionGroup> builder)
    {
        builder.ToTable("user_permission_groups", Schemas.UserService);

        builder.HasKey(u => u.Id);

        builder
            .HasIndex(u => new { u.UserId, u.PermissionGroupId })
            .IsUnique();

        builder.HasOne(o => o.User).WithMany(m => m.UserPermissionGroups).HasForeignKey(t => t.UserId);
        builder.HasOne(o => o.PermissionGroup).WithMany(m => m.UserPermissionGroups).HasForeignKey(t => t.PermissionGroupId);
    }
}
