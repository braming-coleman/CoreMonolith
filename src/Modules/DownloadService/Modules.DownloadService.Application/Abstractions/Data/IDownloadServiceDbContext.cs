using CoreMonolith.Domain.Models.Idempotency;
using Microsoft.EntityFrameworkCore;
using Modules.DownloadService.Domain.Models.DownloadClients;

namespace Modules.DownloadService.Application.Abstractions.Data;

public interface IDownloadServiceDbContext
{
    DbSet<DownloadClient> DownloadClients { get; }

    DbSet<IdempotentRequest> IdempotentRequests { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
