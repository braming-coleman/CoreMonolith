using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.Todos.Get;

public sealed record GetTodosQuery(Guid UserId) : IQuery<List<TodoResponse>>;
