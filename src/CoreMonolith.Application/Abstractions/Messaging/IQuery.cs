using CoreMonolith.SharedKernel.ValueObjects;
using MediatR;

namespace CoreMonolith.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
