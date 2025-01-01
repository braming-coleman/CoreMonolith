using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentValidation;
using Modules.DownloadService.Api.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Domain.Abstractions.Repositories;

namespace Modules.DownloadService.Application.BusinessLogic.SabNzbd.GetHistory;

public sealed record GetApiHistoryQuery(GetRequest Request) : ICommand<GetApiHistoryQueryResult>;

internal class GetApiHistoryQueryValidator : AbstractValidator<GetApiHistoryQuery>
{
    public GetApiHistoryQueryValidator()
    {
        RuleFor(x => x.Request.Mode).NotNull();
        RuleFor(x => x.Request.ApiKey).NotEmpty();
    }
}

public sealed record GetApiHistoryQueryResult(HistoryResponse Response);

internal sealed class GetApiVersionQueryHandler(
    IDownloadClientRepository _downloadClientRepo,
    ISabNzbdClient _sabClient)
    : ICommandHandler<GetApiHistoryQuery, GetApiHistoryQueryResult>
{
    public async Task<Result<GetApiHistoryQueryResult>> Handle(GetApiHistoryQuery request, CancellationToken cancellationToken)
    {
        var clientDetail = await _downloadClientRepo.GetByTypeAsync(DownloadClientType.SabNzbd, cancellationToken);
        if (clientDetail is null)
            return Result.Failure<GetApiHistoryQueryResult>(SabNzbdClientErrors.ConfigNotFound);

        var clientSettings = await clientDetail.GetConfigAsync();
        if (clientSettings is null)
            return Result.Failure<GetApiHistoryQueryResult>(SabNzbdClientErrors.ConfigNotFound);

        if (request.Request.ApiKey != clientSettings.ApiKey)
            return Result.Failure<GetApiHistoryQueryResult>(SabNzbdClientErrors.ApiKeyMismatch);

        var args = new Dictionary<string, string?>
        {
            { "start", request.Request.Start.ToString() },
            { "limit", request.Request.Start.ToString() },
            { "category", request.Request.Category }
        };

        var clientResponse = await _sabClient.GetAsync<HistoryResponse>(request.Request, clientSettings, args, cancellationToken);

        if (clientResponse.IsFailure)
            return Result.Failure<GetApiHistoryQueryResult>(clientResponse.Error);

        if (clientResponse.Value is null)
            return Result.Failure<GetApiHistoryQueryResult>(SabNzbdClientErrors.NullClientResponse);

        return new GetApiHistoryQueryResult(clientResponse.Value);
    }
}