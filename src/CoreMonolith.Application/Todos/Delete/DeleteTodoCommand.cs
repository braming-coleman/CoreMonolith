using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.Todos.Delete;

public sealed record DeleteTodoCommand(Guid TodoItemId) : ICommand;
