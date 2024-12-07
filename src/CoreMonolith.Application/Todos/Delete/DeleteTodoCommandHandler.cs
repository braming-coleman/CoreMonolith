using CoreMonolith.Application.Abstractions.Data;
using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Todos;
using CoreMonolith.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Application.Todos.Delete;

internal sealed class DeleteTodoCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteTodoCommand>
{
    public async Task<Result> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
    {
        TodoItem? todoItem = await context.TodoItems
            .SingleOrDefaultAsync(t => t.Id == command.TodoItemId, cancellationToken);

        if (todoItem == null)
        {
            return Result.Failure(TodoItemErrors.NotFound(command.TodoItemId));
        }

        context.TodoItems.Remove(todoItem);

        todoItem.Raise(new TodoItemDeletedDomainEvent(todoItem.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
