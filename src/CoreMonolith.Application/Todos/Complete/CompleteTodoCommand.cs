using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.Todos.Complete;

public sealed record CompleteTodoCommand(Guid TodoItemId) : ICommand;
