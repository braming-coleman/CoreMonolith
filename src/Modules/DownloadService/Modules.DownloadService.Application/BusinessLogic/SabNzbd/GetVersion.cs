using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentValidation;
using Modules.DownloadService.Api.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Domain.Abstractions.Repositories;

namespace Modules.DownloadService.Application.BusinessLogic.SabNzbd.GetVersion;

public sealed record GetApiVersionQuery(GetRequest Request) : ICommand<GetApiVersionQueryResult>;

internal class GetApiVersionQueryValidator : AbstractValidator<GetApiVersionQuery>
{
    public GetApiVersionQueryValidator()
    {
        RuleFor(x => x.Request.Mode).NotNull();
        RuleFor(x => x.Request.ApiKey).NotEmpty();
    }
}

public sealed record GetApiVersionQueryResult(VersionResponse Response);

internal sealed class GetApiVersionQueryHandler(
    IDownloadClientRepository _downloadClientRepo,
    ISabNzbdClient _sabClient)
    : ICommandHandler<GetApiVersionQuery, GetApiVersionQueryResult>
{
    public async Task<Result<GetApiVersionQueryResult>> Handle(GetApiVersionQuery request, CancellationToken cancellationToken)
    {
        var clientDetail = await _downloadClientRepo.GetByTypeAsync(DownloadClientType.SabNzbd, cancellationToken);
        if (clientDetail is null)
            return Result.Failure<GetApiVersionQueryResult>(SabNzbdClientErrors.ConfigNotFound);

        var clientSettings = await clientDetail.GetConfigAsync();
        if (clientSettings is null)
            return Result.Failure<GetApiVersionQueryResult>(SabNzbdClientErrors.ConfigNotFound);

        if (request.Request.ApiKey != clientSettings.ApiKey)
            return Result.Failure<GetApiVersionQueryResult>(SabNzbdClientErrors.ApiKeyMismatch);

        var clientResponse = await _sabClient.GetAsync<VersionResponse>(request.Request, clientSettings, cancellationToken);

        if (clientResponse.IsFailure)
            return Result.Failure<GetApiVersionQueryResult>(clientResponse.Error);

        if (clientResponse.Value is null)
            return Result.Failure<GetApiVersionQueryResult>(SabNzbdClientErrors.NullClientResponse);

        return new GetApiVersionQueryResult(clientResponse.Value);
    }
}