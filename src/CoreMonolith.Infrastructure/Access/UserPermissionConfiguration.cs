using CoreMonolith.Domain.Access;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreMonolith.Infrastructure.Access;

internal sealed class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ToTable("user_permissions", Schemas.Access);

        builder.HasKey(u => u.Id);

        builder
            .HasIndex(u => new { u.UserId, u.PermissionId })
            .IsUnique();

        builder.HasData(
            new UserPermission { Id = Guid.Parse("f4bf25be-1bfa-430e-8773-912ac312f2f8"), UserId = Guid.Parse("a057e03a-2d3e-4958-9384-dbc529297d89"), PermissionId = Guid.Parse("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07") },
            new UserPermission { Id = Guid.Parse("633afe8b-fd9f-46d7-b94b-9036bdf9e83b"), UserId = Guid.Parse("a057e03a-2d3e-4958-9384-dbc529297d89"), PermissionId = Guid.Parse("87f797e2-92cb-4298-8ec8-1a0d0334837b") },
            new UserPermission { Id = Guid.Parse("1cd7916a-aff3-488d-bdc7-3d9de8877d26"), UserId = Guid.Parse("a057e03a-2d3e-4958-9384-dbc529297d89"), PermissionId = Guid.Parse("b057d9cd-ce76-4d0b-b79d-d10da140a8e8") },
            new UserPermission { Id = Guid.Parse("4a6dee70-f862-464f-b643-b90aeea50706"), UserId = Guid.Parse("a057e03a-2d3e-4958-9384-dbc529297d89"), PermissionId = Guid.Parse("d69712b8-195b-4c62-ae66-d4b37702a23d") });
    }
}
