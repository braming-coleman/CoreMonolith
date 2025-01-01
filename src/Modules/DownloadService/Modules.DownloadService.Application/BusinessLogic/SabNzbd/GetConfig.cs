using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentValidation;
using Modules.DownloadService.Api.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Domain.Abstractions.Repositories;

namespace Modules.DownloadService.Application.BusinessLogic.SabNzbd.GetConfig;

public sealed record GetApiConfigQuery(GetRequest Request) : ICommand<GetApiConfigQueryResult>;

internal class GetApiConfigQueryValidator : AbstractValidator<GetApiConfigQuery>
{
    public GetApiConfigQueryValidator()
    {
        RuleFor(x => x.Request.Mode).NotNull();
        RuleFor(x => x.Request.ApiKey).NotEmpty();
    }
}

public sealed record GetApiConfigQueryResult(ConfigResponse Response);

internal sealed class GetApiVersionQueryHandler(
    IDownloadClientRepository _downloadClientRepo,
    ISabNzbdClient _sabClient)
    : ICommandHandler<GetApiConfigQuery, GetApiConfigQueryResult>
{
    public async Task<Result<GetApiConfigQueryResult>> Handle(GetApiConfigQuery request, CancellationToken cancellationToken)
    {
        var clientDetail = await _downloadClientRepo.GetByTypeAsync(DownloadClientType.SabNzbd, cancellationToken);
        if (clientDetail is null)
            return Result.Failure<GetApiConfigQueryResult>(SabNzbdClientErrors.ConfigNotFound);

        var clientSettings = await clientDetail.GetConfigAsync();
        if (clientSettings is null)
            return Result.Failure<GetApiConfigQueryResult>(SabNzbdClientErrors.ConfigNotFound);

        if (request.Request.ApiKey != clientSettings.ApiKey)
            return Result.Failure<GetApiConfigQueryResult>(SabNzbdClientErrors.ApiKeyMismatch);

        var clientResponse = await _sabClient.GetAsync<ConfigResponse>(request.Request, clientSettings, cancellationToken: cancellationToken);

        if (clientResponse.IsFailure)
            return Result.Failure<GetApiConfigQueryResult>(clientResponse.Error);

        if (clientResponse.Value is null)
            return Result.Failure<GetApiConfigQueryResult>(SabNzbdClientErrors.NullClientResponse);

        return new GetApiConfigQueryResult(clientResponse.Value);
    }
}