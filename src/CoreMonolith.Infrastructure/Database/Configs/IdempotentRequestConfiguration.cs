using CoreMonolith.Domain.Models.Idempotency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreMonolith.Infrastructure.Database.Configs;

internal sealed class IdempotentRequestConfiguration : IEntityTypeConfiguration<IdempotentRequest>
{
    public void Configure(EntityTypeBuilder<IdempotentRequest> builder)
    {
        builder.ToTable("idempotent_requests", Schemas.Default);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name).IsRequired();
    }
}
