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
            new Permission { Id = Guid.Parse("0193eb73-d636-750a-a839-5571f30fd6b2"), Key = ApiPermissions.UserRead, Description = "Read access to [user] resource" },
            new Permission { Id = Guid.Parse("0193eb73-d636-7aed-bbbc-963672568d66"), Key = ApiPermissions.UserWrite, Description = "Write access to [user] resource" },
            new Permission { Id = Guid.Parse("0193eb73-d636-75c6-bc6b-69e79f6b2586"), Key = ApiPermissions.TodoRead, Description = "Read access to [todo] resource" },
            new Permission { Id = Guid.Parse("0193eb73-d636-7a87-8b5e-c84002f9edfd"), Key = ApiPermissions.TodoWrite, Description = "Write access to [todo] resource" },
            new Permission { Id = Guid.Parse("0193eb73-d636-770f-9d4d-6f2c6d9ccac3"), Key = ApiPermissions.UserPermissionRead, Description = "Read access to [user-permission] resource" },
            new Permission { Id = Guid.Parse("0193eb73-d636-79e6-b669-87236dbbaa96"), Key = ApiPermissions.UserPermissionWrite, Description = "Write access to [user-permission] resource" },
            new Permission { Id = Guid.Parse("0193eb73-d636-72c1-b3fb-52c82f3593ac"), Key = ApiPermissions.PermissionRead, Description = "Read access to [permission] resource" },
            new Permission { Id = Guid.Parse("0193eb73-d636-7150-a2bc-13bde0f65734"), Key = ApiPermissions.PermissionWrite, Description = "Write access to [permission] resource" });
    }
}
