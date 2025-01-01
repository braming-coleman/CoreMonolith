using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentValidation;
using Modules.DownloadService.Api.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Domain.Abstractions.Repositories;

namespace Modules.DownloadService.Application.BusinessLogic.SabNzbd.GetFullStatus;

public sealed record GetApiFullStatusQuery(GetRequest Request) : ICommand<GetApiFullStatusQueryResult>;

internal class GetApiFullStatusQueryValidator : AbstractValidator<GetApiFullStatusQuery>
{
    public GetApiFullStatusQueryValidator()
    {
        RuleFor(x => x.Request.Mode).NotNull();
        RuleFor(x => x.Request.ApiKey).NotEmpty();
    }
}

public sealed record GetApiFullStatusQueryResult(FullStatusResponse Response);

internal sealed class GetApiVersionQueryHandler(
    IDownloadClientRepository _downloadClientRepo,
    ISabNzbdClient _sabClient)
    : ICommandHandler<GetApiFullStatusQuery, GetApiFullStatusQueryResult>
{
    public async Task<Result<GetApiFullStatusQueryResult>> Handle(GetApiFullStatusQuery request, CancellationToken cancellationToken)
    {
        var clientDetail = await _downloadClientRepo.GetByTypeAsync(DownloadClientType.SabNzbd, cancellationToken);
        if (clientDetail is null)
            return Result.Failure<GetApiFullStatusQueryResult>(SabNzbdClientErrors.ConfigNotFound);

        var clientSettings = await clientDetail.GetConfigAsync();
        if (clientSettings is null)
            return Result.Failure<GetApiFullStatusQueryResult>(SabNzbdClientErrors.ConfigNotFound);

        if (request.Request.ApiKey != clientSettings.ApiKey)
            return Result.Failure<GetApiFullStatusQueryResult>(SabNzbdClientErrors.ApiKeyMismatch);

        var clientResponse = await _sabClient.GetAsync<FullStatusResponse>(request.Request, clientSettings, cancellationToken: cancellationToken);

        if (clientResponse.IsFailure)
            return Result.Failure<GetApiFullStatusQueryResult>(clientResponse.Error);

        if (clientResponse.Value is null)
            return Result.Failure<GetApiFullStatusQueryResult>(SabNzbdClientErrors.NullClientResponse);

        return new GetApiFullStatusQueryResult(clientResponse.Value);
    }
}