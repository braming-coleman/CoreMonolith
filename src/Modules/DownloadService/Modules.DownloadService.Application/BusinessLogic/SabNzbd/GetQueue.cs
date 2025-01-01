using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentValidation;
using Modules.DownloadService.Api.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Domain.Abstractions.Repositories;

namespace Modules.DownloadService.Application.BusinessLogic.SabNzbd.GetQueue;

public sealed record GetApiQueueQuery(GetRequest Request) : ICommand<GetApiQueueQueryResult>;

internal class GetApiQueueQueryValidator : AbstractValidator<GetApiQueueQuery>
{
    public GetApiQueueQueryValidator()
    {
        RuleFor(x => x.Request.Mode).NotNull();
        RuleFor(x => x.Request.ApiKey).NotEmpty();
    }
}

public sealed record GetApiQueueQueryResult(QueueResponse Response);

internal sealed class GetApiVersionQueryHandler(
    IDownloadClientRepository _downloadClientRepo,
    ISabNzbdClient _sabClient)
    : ICommandHandler<GetApiQueueQuery, GetApiQueueQueryResult>
{
    public async Task<Result<GetApiQueueQueryResult>> Handle(GetApiQueueQuery request, CancellationToken cancellationToken)
    {
        var clientDetail = await _downloadClientRepo.GetByTypeAsync(DownloadClientType.SabNzbd, cancellationToken);
        if (clientDetail is null)
            return Result.Failure<GetApiQueueQueryResult>(SabNzbdClientErrors.ConfigNotFound);

        var clientSettings = await clientDetail.GetConfigAsync();
        if (clientSettings is null)
            return Result.Failure<GetApiQueueQueryResult>(SabNzbdClientErrors.ConfigNotFound);

        if (request.Request.ApiKey != clientSettings.ApiKey)
            return Result.Failure<GetApiQueueQueryResult>(SabNzbdClientErrors.ApiKeyMismatch);

        var args = new Dictionary<string, string?>
        {
            { "start", request.Request.Start.ToString() },
            { "limit", request.Request.Start.ToString() },
            { "category", request.Request.Category }
        };

        var clientResponse = await _sabClient.GetAsync<QueueResponse>(request.Request, clientSettings, args, cancellationToken);

        if (clientResponse.IsFailure)
            return Result.Failure<GetApiQueueQueryResult>(clientResponse.Error);

        if (clientResponse.Value is null)
            return Result.Failure<GetApiQueueQueryResult>(SabNzbdClientErrors.NullClientResponse);

        return new GetApiQueueQueryResult(clientResponse.Value);
    }
}