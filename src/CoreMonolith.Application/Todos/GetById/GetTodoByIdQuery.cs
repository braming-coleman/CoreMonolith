using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.Todos.GetById;

public sealed record GetTodoByIdQuery(Guid TodoItemId) : IQuery<TodoResponse>;
