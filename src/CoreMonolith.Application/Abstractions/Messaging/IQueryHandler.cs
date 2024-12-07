using CoreMonolith.SharedKernel;
using MediatR;

namespace CoreMonolith.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
