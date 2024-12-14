using CoreMonolith.Domain.Access;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreMonolith.Infrastructure.DataSourceConfigs.Access;

internal sealed class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ToTable("user_permissions", Schemas.Access);

        builder.HasKey(u => u.Id);

        builder
            .HasIndex(u => new { u.UserId, u.PermissionId })
            .IsUnique();

        builder.HasOne(o => o.User).WithMany(m => m.UserPermissions).HasForeignKey(t => t.UserId);
        builder.HasOne(o => o.Permission).WithMany(m => m.UserPermissions).HasForeignKey(t => t.PermissionId);

        var testUserId = Guid.Parse("a057e03a-2d3e-4958-9384-dbc529297d89");
        var webAppServiceAccount = Guid.Parse("b75e4ad1-0804-427b-abd9-a966e2d12266");

        builder.HasData(
            //test@test.com
            new UserPermission { Id = Guid.Parse("f4bf25be-1bfa-430e-8773-912ac312f2f8"), UserId = testUserId, PermissionId = Guid.Parse("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07") },
            new UserPermission { Id = Guid.Parse("633afe8b-fd9f-46d7-b94b-9036bdf9e83b"), UserId = testUserId, PermissionId = Guid.Parse("87f797e2-92cb-4298-8ec8-1a0d0334837b") },
            new UserPermission { Id = Guid.Parse("1cd7916a-aff3-488d-bdc7-3d9de8877d26"), UserId = testUserId, PermissionId = Guid.Parse("b057d9cd-ce76-4d0b-b79d-d10da140a8e8") },
            new UserPermission { Id = Guid.Parse("4a6dee70-f862-464f-b643-b90aeea50706"), UserId = testUserId, PermissionId = Guid.Parse("d69712b8-195b-4c62-ae66-d4b37702a23d") },
            new UserPermission { Id = Guid.Parse("610f21e7-77e6-4398-8be2-0330c8111143"), UserId = testUserId, PermissionId = Guid.Parse("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6") },
            new UserPermission { Id = Guid.Parse("43b1a7d7-97fb-4b27-be30-a61724e6300c"), UserId = testUserId, PermissionId = Guid.Parse("c9a0b1be-391b-44aa-90d8-aca0757d18d6") },
            //service-account@download-manager.com
            new UserPermission { Id = Guid.Parse("8fdfbd52-6f5b-40b0-a86b-2624478de3b6"), UserId = webAppServiceAccount, PermissionId = Guid.Parse("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07") },
            new UserPermission { Id = Guid.Parse("693c920b-dd14-4654-9c48-c0a6aa0df11c"), UserId = webAppServiceAccount, PermissionId = Guid.Parse("87f797e2-92cb-4298-8ec8-1a0d0334837b") },
            new UserPermission { Id = Guid.Parse("1734a74d-14c1-4dc2-9332-4091ba9c7c56"), UserId = webAppServiceAccount, PermissionId = Guid.Parse("b057d9cd-ce76-4d0b-b79d-d10da140a8e8") },
            new UserPermission { Id = Guid.Parse("354bdea6-7847-4a21-aeb7-52b39be4719a"), UserId = webAppServiceAccount, PermissionId = Guid.Parse("d69712b8-195b-4c62-ae66-d4b37702a23d") },
            new UserPermission { Id = Guid.Parse("5c9b4ceb-0591-484f-8ffd-803deb266c7a"), UserId = webAppServiceAccount, PermissionId = Guid.Parse("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6") },
            new UserPermission { Id = Guid.Parse("8bbd1b7e-b91d-46bf-b746-3722376c8186"), UserId = webAppServiceAccount, PermissionId = Guid.Parse("c9a0b1be-391b-44aa-90d8-aca0757d18d6") });
    }
}
