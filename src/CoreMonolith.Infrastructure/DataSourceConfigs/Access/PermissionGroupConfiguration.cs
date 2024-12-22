using CoreMonolith.Domain.Models.Access.PermissionGroups;
using CoreMonolith.Infrastructure.Database;
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
    }
}
