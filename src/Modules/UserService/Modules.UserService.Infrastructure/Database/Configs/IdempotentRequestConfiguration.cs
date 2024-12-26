using CoreMonolith.Domain.Models.Idempotency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.UserService.Infrastructure.Database.Configs;

internal sealed class IdempotentRequestConfiguration : IEntityTypeConfiguration<IdempotentRequest>
{
    public void Configure(EntityTypeBuilder<IdempotentRequest> builder)
    {
        builder.ToTable("idempotent_requests", Schemas.UserService);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name).IsRequired();
    }
}
