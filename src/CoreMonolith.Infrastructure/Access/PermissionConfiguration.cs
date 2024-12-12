using CoreMonolith.Domain.Access;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreMonolith.Infrastructure.Access;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions", Schemas.Access);

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Key).IsUnique();

        builder.HasData(
            new Permission { Id = Guid.Parse("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07"), Key = "user:read", Description = "Read access to [user] resource" },
            new Permission { Id = Guid.Parse("87f797e2-92cb-4298-8ec8-1a0d0334837b"), Key = "user:write", Description = "Write access to [user] resource" },
            new Permission { Id = Guid.Parse("b057d9cd-ce76-4d0b-b79d-d10da140a8e8"), Key = "todo:read", Description = "Read access to [todo] resource" },
            new Permission { Id = Guid.Parse("d69712b8-195b-4c62-ae66-d4b37702a23d"), Key = "todo:write", Description = "Write access to [todo] resource" });
    }
}
