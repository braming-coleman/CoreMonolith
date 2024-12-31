using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentValidation;
using Modules.DownloadService.Api.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Domain.Abstractions.Repositories;

namespace Modules.DownloadService.Application.BusinessLogic.SabNzbd.GetConfig;

public sealed record GetApiGetConfigQuery(GetRequest Request) : ICommand<GetApiGetConfigQueryResult>;

internal class GetApiGetConfigQueryValidator : AbstractValidator<GetApiGetConfigQuery>
{
    public GetApiGetConfigQueryValidator()
    {
        RuleFor(x => x.Request.Mode).NotNull();
        RuleFor(x => x.Request.ApiKey).NotEmpty();
    }
}

public sealed record GetApiGetConfigQueryResult(ConfigResponse Response);

internal sealed class GetApiVersionQueryHandler(
    IDownloadClientRepository _downloadClientRepo,
    ISabNzbdClient _sabClient)
    : ICommandHandler<GetApiGetConfigQuery, GetApiGetConfigQueryResult>
{
    public async Task<Result<GetApiGetConfigQueryResult>> Handle(GetApiGetConfigQuery request, CancellationToken cancellationToken)
    {
        var clientDetail = await _downloadClientRepo.GetByTypeAsync(DownloadClientType.SabNzbd, cancellationToken);
        if (clientDetail is null)
            return Result.Failure<GetApiGetConfigQueryResult>(SabNzbdClientErrors.ConfigNotFound);

        var clientSettings = await clientDetail.GetConfigAsync();
        if (clientSettings is null)
            return Result.Failure<GetApiGetConfigQueryResult>(SabNzbdClientErrors.ConfigNotFound);

        if (request.Request.ApiKey != clientSettings.ApiKey)
            return Result.Failure<GetApiGetConfigQueryResult>(SabNzbdClientErrors.ApiKeyMismatch);

        var clientResponse = await _sabClient.GetAsync<ConfigResponse>(request.Request, clientSettings, cancellationToken);

        if (clientResponse.IsFailure)
            return Result.Failure<GetApiGetConfigQueryResult>(clientResponse.Error);

        if (clientResponse.Value is null)
            return Result.Failure<GetApiGetConfigQueryResult>(SabNzbdClientErrors.NullClientResponse);

        return new GetApiGetConfigQueryResult(clientResponse.Value);
    }
}