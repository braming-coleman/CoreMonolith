using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.UserService.Domain.Models.PermissionGroupPermissions;

namespace Modules.UserService.Infrastructure.Database.Configs;

internal sealed class PermissionGroupPermissionConfiguration : IEntityTypeConfiguration<PermissionGroupPermission>
{
    public void Configure(EntityTypeBuilder<PermissionGroupPermission> builder)
    {
        builder.ToTable("permission_group_permissions", Schemas.UserService);

        builder.HasKey(u => u.Id);

        builder
            .HasIndex(u => new { u.PermissionId, u.PermissionGroupId })
            .IsUnique();

        builder.HasOne(o => o.Permission).WithMany(m => m.PermissionGroupPermissions).HasForeignKey(t => t.PermissionId);
        builder.HasOne(o => o.PermissionGroup).WithMany(m => m.PermissionGroupPermissions).HasForeignKey(t => t.PermissionGroupId);

        builder.HasData(
            new PermissionGroupPermission { Id = Guid.Parse("0193ec2f-13a3-7c0c-af57-eaeab6d451d9"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_Admin, PermissionId = AccessSeedDataConstants.PermissionId_UserRead },
            new PermissionGroupPermission { Id = Guid.Parse("0193ec2f-13a3-70f6-bba2-5e63aaa12173"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_Admin, PermissionId = AccessSeedDataConstants.PermissionId_UserWrite },
            new PermissionGroupPermission { Id = Guid.Parse("0193ec2f-13a3-7a5c-83fb-d2b822708e25"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_Admin, PermissionId = AccessSeedDataConstants.PermissionId_PermissionGroupRead },
            new PermissionGroupPermission { Id = Guid.Parse("0193ec2f-13a3-7c6d-a09d-4948b5dc81e2"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_Admin, PermissionId = AccessSeedDataConstants.PermissionId_PermissionGroupWrite },
            new PermissionGroupPermission { Id = Guid.Parse("0193ec2f-13a3-7c79-adfa-97611eac8222"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_Admin, PermissionId = AccessSeedDataConstants.PermissionId_PermissionRead },
            new PermissionGroupPermission { Id = Guid.Parse("0193ec2f-13a3-70fd-8d28-2b103e51e8ae"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_Admin, PermissionId = AccessSeedDataConstants.PermissionId_PermissionWrite },
            new PermissionGroupPermission { Id = Guid.Parse("0193ed22-aa79-7d79-8de1-f4ade9cdfa4d"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_Admin, PermissionId = AccessSeedDataConstants.PermissionId_WeatherRead },
            new PermissionGroupPermission { Id = Guid.Parse("0193f4d5-fba9-7010-bd28-fa71072a4950"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_Admin, PermissionId = AccessSeedDataConstants.PermissionId_ApiGatewayAccess },

            new PermissionGroupPermission { Id = Guid.Parse("0193ec2f-13a3-79e7-acd9-861e9bf5d943"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_User, PermissionId = AccessSeedDataConstants.PermissionId_UserRead },
            new PermissionGroupPermission { Id = Guid.Parse("0193ec2f-13a3-77e6-9b8b-81e474ef00dc"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_User, PermissionId = AccessSeedDataConstants.PermissionId_UserWrite },
            new PermissionGroupPermission { Id = Guid.Parse("0193ec2f-13a3-7127-b499-fcef873fa5db"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_User, PermissionId = AccessSeedDataConstants.PermissionId_PermissionRead },
            new PermissionGroupPermission { Id = Guid.Parse("0193ed22-aa79-7e19-be3c-504b6c0a244b"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_User, PermissionId = AccessSeedDataConstants.PermissionId_WeatherRead },
            new PermissionGroupPermission { Id = Guid.Parse("0193f4d5-fba9-733a-a8ff-3b920656c4db"), PermissionGroupId = AccessSeedDataConstants.PermissionGroupId_User, PermissionId = AccessSeedDataConstants.PermissionId_ApiGatewayAccess });
    }
}
