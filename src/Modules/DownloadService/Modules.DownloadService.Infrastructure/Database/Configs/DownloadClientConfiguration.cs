using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.DownloadService.Domain.Models.DownloadClients;

namespace Modules.DownloadService.Infrastructure.Database.Configs;

internal sealed class DownloadClientConfiguration : IEntityTypeConfiguration<DownloadClient>
{
    public void Configure(EntityTypeBuilder<DownloadClient> builder)
    {
        builder.ToTable("download_clients", Schemas.DownloadService);

        builder.HasKey(u => u.Id);

        builder
            .HasIndex(u => u.Type)
            .IsUnique();

        builder.Property(u => u.ConfigString).HasColumnType("jsonb");
    }
}
