using CoreMonolith.Domain.Abstractions.Messaging;

namespace Modules.DownloadService.Domain.Models.DownloadClients;

public sealed record DownloadClientCreatedDomainEvent(Guid ClientId) : IDomainEvent;
