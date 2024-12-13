using Microsoft.AspNetCore.Routing;

namespace CoreMonolith.SharedKernel.Abstractions;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
