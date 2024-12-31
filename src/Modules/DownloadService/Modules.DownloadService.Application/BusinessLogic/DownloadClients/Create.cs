using CoreMonolith.Application.Abstractions.Idempotency;
using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Helpers;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Logging;
using Modules.DownloadService.Api.Models;
using Modules.DownloadService.Application.Clients.SabNzbd.Models;
using Modules.DownloadService.Domain.Abstractions.Repositories;
using Modules.DownloadService.Domain.Models.DownloadClients;

namespace Modules.DownloadService.Application.BusinessLogic.DownloadClients.Create;

public sealed record CreateDownloadClientCommand(
    Guid RequestId,
    DownloadClientType ClientType,
    string Name,
    bool Enabled,
    object Config)
    : IdempotentCommand<Guid>(RequestId);

internal class CreateDownloadClientCommandValidator : AbstractValidator<CreateDownloadClientCommand>
{
    public CreateDownloadClientCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ClientType).IsInEnum();
        RuleFor(x => x.Config).NotNull();
    }
}

internal sealed class CreateDownloadClientCommandHandler(
    IDownloadClientRepository _downloadClientRepo,
    IDownloadServiceUow _unitOfWork)
    : ICommandHandler<CreateDownloadClientCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateDownloadClientCommand request, CancellationToken cancellationToken)
    {
        if (await _downloadClientRepo.ExistsByType(request.ClientType, cancellationToken))
            return Result.Failure<Guid>(DownloadClientErrors.ConfigExists(request.ClientType.ToString()));

        var configString = await GetConfigString(request.ClientType, request.Config);
        if (configString.IsFailure)
            return Result.Failure<Guid>(configString.Error);

        var client = new DownloadClient
        {
            Id = Guid.CreateVersion7(),
            Type = request.ClientType,
            Name = request.Name,
            ConfigString = configString.Value,
            Enabled = request.Enabled
        };

        client.Raise(new DownloadClientCreatedDomainEvent(client.Id));

        await _downloadClientRepo.AddAsync(client, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return client.Id;
    }

    private static async Task<Result<string>> GetConfigString(DownloadClientType type, object config)
    {
        if (type == DownloadClientType.SabNzbd)
        {
            var settings = await JsonHelper.DeserializeAsync<SabNzbdClientSettings>(config.ToString()!);

            if (settings == null || string.IsNullOrEmpty(settings.Host))
                return Result.Failure<string>(DownloadClientErrors.ConfigFailed(type.ToString()));

            return await JsonHelper.SerializeAsync(settings);
        }

        return Result.Failure<string>(DownloadClientErrors.ConfigUnsupported(type.ToString()));
    }
}

internal sealed class DownloadClientCreatedDomainEventHandler(
    IOutputCacheStore _cacheStore,
    ILogger<DownloadClientCreatedDomainEventHandler> _logger)
    : INotificationHandler<DownloadClientCreatedDomainEvent>
{
    public async Task Handle(DownloadClientCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning("DownloadClient created - {Notification}", notification);

        await _cacheStore.EvictByTagAsync(Tags.DownloadClient, cancellationToken);
    }
}
