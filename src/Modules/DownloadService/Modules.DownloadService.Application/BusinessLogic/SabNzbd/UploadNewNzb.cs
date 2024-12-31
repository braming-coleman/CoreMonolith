using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.DownloadService.Api.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Domain.Abstractions.Repositories;
using Modules.DownloadService.Domain.Models.DownloadClients;

namespace Modules.DownloadService.Application.BusinessLogic.SabNzbd.UploadNewNzb;

public sealed record UploadNewNzbCommand(
    NzbUploadRequest UploadRequest)
    : ICommand<UploadNewNzbCommandResult>;

internal class UploadNewNzbCommandValidator : AbstractValidator<UploadNewNzbCommand>
{
    public UploadNewNzbCommandValidator()
    {
        RuleFor(x => x.UploadRequest.File).NotNull();
        RuleFor(x => x.UploadRequest.ApiKey).NotEmpty();
    }
}

public sealed record UploadNewNzbCommandResult(NzbUploadResponse UploadResult);

internal sealed class UploadNewNzbCommandHandler(
    IDownloadClientRepository _downloadClientRepo,
    ISabNzbdClient _sabClient,
    IPublisher _publisher)
    : ICommandHandler<UploadNewNzbCommand, UploadNewNzbCommandResult>
{
    public async Task<Result<UploadNewNzbCommandResult>> Handle(UploadNewNzbCommand request, CancellationToken cancellationToken)
    {
        var clientDetail = await _downloadClientRepo.GetByTypeAsync(DownloadClientType.SabNzbd, cancellationToken);
        if (clientDetail is null)
            return Result.Failure<UploadNewNzbCommandResult>(SabNzbdClientErrors.ConfigNotFound);

        var clientSettings = await clientDetail.GetConfigAsync();
        if (clientSettings is null)
            return Result.Failure<UploadNewNzbCommandResult>(SabNzbdClientErrors.ConfigNotFound);

        if (request.UploadRequest.ApiKey != clientSettings.ApiKey)
            return Result.Failure<UploadNewNzbCommandResult>(SabNzbdClientErrors.ApiKeyMismatch);

        var uploadRequest = request.UploadRequest;

        if (DefaultRequest(request.UploadRequest))
        {
            uploadRequest = new NzbUploadRequest(
                request.UploadRequest.ApiKey,
                request.UploadRequest.File,
                request.UploadRequest.NzbName,
                clientSettings.Category,
                clientSettings.Priority,
                clientSettings.PostPorcesssing);
        }

        var clientResponse = await _sabClient.UploadNzbAsync(uploadRequest, clientSettings, cancellationToken);

        if (clientResponse.IsFailure)
            return Result.Failure<UploadNewNzbCommandResult>(clientResponse.Error);

        if (clientResponse.Value is null)
            return Result.Failure<UploadNewNzbCommandResult>(SabNzbdClientErrors.NullClientResponse);

        await _publisher.Publish(new NewNzbUploadedToSabNzbdDomainEvent(clientResponse.Value.UploadIds.FirstOrDefault() ?? "not_found"), cancellationToken);

        var finalResponse = new UploadNewNzbCommandResult(
            clientResponse.Value.Adapt<NzbUploadResponse>());

        return finalResponse;
    }

    private bool DefaultRequest(NzbUploadRequest request)
    {
        return
            request.Category == SabNzdbDefaults.Category &&
            request.Priority == SabNzdbDefaults.Priority &&
            request.PostProcessing == SabNzdbDefaults.PostProcessingOptions;
    }
}

internal sealed class NewNzbUploadedToSabNzbdDomainEventHandler(
    ILogger<NewNzbUploadedToSabNzbdDomainEventHandler> _logger)
    : INotificationHandler<NewNzbUploadedToSabNzbdDomainEvent>
{
    public Task Handle(NewNzbUploadedToSabNzbdDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning("Nzb uploaded - {Notification}", notification);

        return Task.CompletedTask;
    }
}