using CoreMonolith.Domain.Models.Access.Permissions;
using CoreMonolith.Infrastructure.Database;
using CoreMonolith.SharedKernel.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreMonolith.Infrastructure.DataSourceConfigs.Access;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions", Schemas.Access);

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Key).IsUnique();

        builder.HasData(
            new Permission { Id = Guid.Parse("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07"), Key = ApiPermissions.UserRead, Description = "Read access to [user] resource" },
            new Permission { Id = Guid.Parse("87f797e2-92cb-4298-8ec8-1a0d0334837b"), Key = ApiPermissions.UserWrite, Description = "Write access to [user] resource" },
            new Permission { Id = Guid.Parse("b057d9cd-ce76-4d0b-b79d-d10da140a8e8"), Key = ApiPermissions.TodoRead, Description = "Read access to [todo] resource" },
            new Permission { Id = Guid.Parse("d69712b8-195b-4c62-ae66-d4b37702a23d"), Key = ApiPermissions.TodoWrite, Description = "Write access to [todo] resource" },
            new Permission { Id = Guid.Parse("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6"), Key = ApiPermissions.UserPermissionRead, Description = "Read access to [user-permission] resource" },
            new Permission { Id = Guid.Parse("c9a0b1be-391b-44aa-90d8-aca0757d18d6"), Key = ApiPermissions.UserPermissionWrite, Description = "Write access to [user-permission] resource" },
            new Permission { Id = Guid.Parse("86c9629c-c5e5-49ad-94b7-6981f921386b"), Key = ApiPermissions.PermissionRead, Description = "Read access to [permission] resource" },
            new Permission { Id = Guid.Parse("82fddd77-f662-4927-aae1-46ebb00c2c05"), Key = ApiPermissions.PermissionWrite, Description = "Write access to [permission] resource" });
    }
}
