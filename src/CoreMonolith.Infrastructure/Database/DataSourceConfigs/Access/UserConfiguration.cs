using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreMonolith.Infrastructure.DataSourceConfigs.Access;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", Schemas.Access);

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.ExternalId).IsUnique();

        builder.HasIndex(u => u.Email).IsUnique();
    }
}
